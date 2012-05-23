/*  
Author: Matt Gramolini
Company: Genuine
Date: 11/26/11

VForms is a module that automagically wires up Forms to Webservices
It incorporates and simplifies many features of AspNetSitecoreForms.js
To override event handlers, simply add code after this file like...

VForms.forms.formId.events.oneventname = function(){ do stuff here }
    
ex.
VForms.forms.editProfile.events.oncomplete = function(){ alert("ajaxComplete") }

Tims Notes
binds to data-init and data-serviceurl on <form tag>
<form id="form-id" data-init="forms" data-serviceurl></form>

*id corresponds to VForms.forms.->editProfile<-
*data-init corresponds to VForms.->forms<-
*data-serviceurl corresponds to the webservice URL you want the form to submit to


how to use 
$(document).ready(UTIL_VForms.loadEvents());
$(document).ready(UTIL_VForms.loadEvents($, { storeCookies: true }));
$(document).ready(UTIL_VForms.loadEvents($, { validate: true, transform: true }));
$(document).ready(UTIL_VForms.loadEvents($, { tags: 'form, div' }));
$(document).ready(UTIL_VForms.loadEvents($, { ids: '#departments, #roles' }));

*/
window.VForms = {};

VForms.common = (function () {
    var initialized = false;
    this.debug = false;
    var init = function (args) {
        if (!initialized) {
            if (args) {
                this.debug = args.debug;
                this.validationSelector = args.vmSelector
            ? args.vmSelector
            : '#validation-messages';
            }
            else {
                this.validationSelector = '#validation-messages';
            }

            VForms.common.log('vforms common init');
            initialized = true;
        }
    },
        log = function (str) {
            if (this.debug && window.console) {
                console.log(str);
            }
        },
        loadWithOptions = function (options) {
            if (options !== undefined || options !== '') {
                UTIL_VForms.loadEvents($, { tags: options.tags, ids: options.ids, validate: options.validate, transform: options.transform });
            }
        },
        loadResponse = function (data) {
            var json = typeof data.d === 'string'
                ? $.parseJSON(data.d)
                : data.d;

            if (!json.Data || !json.Properties) {

                // handle no vJsonResponse
                return false;
            }

            var vJsonResponse = {};

            json.Data.length
                ? (vJsonResponse.Data = {}, $.each(json.Data, function () { vJsonResponse.Data[this.Key] = this.Value; }))
                : vJsonResponse.Data = json.Data;

            json.Properties.length
                ? (vJsonResponse.Properties = {}, $.each(json.Properties, function () { vJsonResponse.Properties[this.Key] = this.Value; }))
                : vJsonResponse.Properties = json.Properties;

            return vJsonResponse;
        },
        handleResponse = function (events, vJsonResponse, id) {

            if (vJsonResponse.Properties) {

                // task is complete, is Valid
                if (vJsonResponse.Properties.TaskCompleted && vJsonResponse.Properties.TaskDataIsValid) {

                    if (typeof events.onvalid === 'function') {
                        events.onvalid(vJsonResponse, id);
                    }
                    else {
                        for (var i = 0; i < events.onvalid.length; i++) {
                            events.onvalid[i].call(events, vJsonResponse, id);
                        }
                    }
                    return true;
                }

                // task is not complete, was Aborted, or data invalid
                if (!vJsonResponse.Properties.TaskCompleted || vJsonResponse.Properties.TaskAborted || !vJsonResponse.Properties.TaskDataIsValid) {

                    if (typeof events.onfailure === 'function') {
                        events.onfailure(vJsonResponse, id);
                    }
                    else {
                        for (i = 0; i < events.onfailure.length; i++) {
                            events.onfailure[i].call(events, vJsonResponse, id);
                        }
                    }
                    return true;
                }

                // task not complete, not aborted
                if (!vJsonResponse.Properties.TaskCompleted && !vJsonResponse.Properties.TaskAborted) {
                    // not sure if we will be keeping this section, it should probably go under aborted...?
                    // we need a meeting about sending back informational or html messages.
                    var msg = "Error: No message was supplied";
                    //TODO: we need to look for all possible messages
                    if (vJsonResponse.Data.Message) {
                        msg = vJsonResponse.Data.Message;
                    }
                    else if (vJsonResponse.Data.DefaultInformationalMessage) {
                        msg = vJsonResponse.Data.DefaultInformationalMessage;
                    }

                    $(VForms.common.validationSelector).html(msg).modal({ containerId: 'errorContainer', overlayClose: true, overlayId: 'errorOverlay', closeHTML: '<a>X</a>' });
                }
            }

            return false;
        },
        post = function (events, jsonData, url, id) {
            if (events.iswaiting === false) {
                $.ajax({
                    url: url,
                    type: "POST",
                    contentType: "application/json; charset=utf-8",
                    data: "{ 'json': '" + jsonData + "'}",
                    success: function (data, textStatus, xhr) {
                        if (typeof events.onsuccess === 'function') {
                            events.onsuccess(data, textStatus, xhr, id);
                        }
                        else {
                            for (var i = 0; i < events.onsuccess.length; i++) {
                                events.onsuccess[i].call(events, data, textStatus, xhr, id);
                            }
                        }
                    },
                    error: function (xhr, textStatus, errorThrown) {
                        if (typeof events.onerror === 'function') {
                            events.onerror(xhr, textStatus, errorThrown, id);
                        }
                        else {
                            for (var i = 0; i < events.onerror.length; i++) {
                                events.onerror[i].call(events, xhr, textStatus, errorThrown, id);
                            }
                        }
                    },
                    complete: function (xhr, textStatus) {
                        if (typeof events.oncomplete === 'function') {
                            events.oncomplete(xhr, textStatus, id);
                        }
                        else {
                            for (var i = 0; i < events.oncomplete.length; i++) {
                                events.oncomplete[i].call(events, xhr, textStatus, id);
                            }
                        }
                    }
                });

                events.iswaiting = true;
            }
            else {
                $(VForms.common.validationSelector).html('<p>Currently awaiting reply from server...Please wait</p>').modal({ containerId: 'errorContainer', overlayClose: true, overlayId: 'errorOverlay', closeHTML: '<a>X</a>' });
            }
        };
    return {
        init: init,
        log: log,
        loadWithOptions: loadWithOptions,
        loadResponse: loadResponse,
        handleResponse: handleResponse,
        post: post,
        debug: this.debug
    };
} ());

// data-init = class, use "forms"
// data-serviceurl = web service url ex. /App_Webservices/Public/WebServices.asmx/ServiceName
// data-targetid = target id for populating data, otherwise uses self
VForms.forms = (function () {
    var current;

    var init = function (args) {

        var element = args.element;
        var options = args.options;

        current = this[element.id] = {
            node: element,
            options: options,
            getData: getData,
            events: {
                onsubmit: submitHandler,
                onsuccess: [successHandler],
                onerror: [errorHandler],
                oncomplete: [completeHandler],
                onvalid: [handleSuccess],
                onfailure: [handleFailure],
                iswaiting: false
            }
        };

        VForms.common.log('form #' + element.id + ' init');

        var pluginValue = $(current.node).data('plugin');

        fixInputNames();
        checkForUploads();
        // TODO: this may be deprecated in default options
        appendSerializedField();

        if (options.storeCookies) {
            scrollToLocation();
            loadSavedData();
        }

        if (options.validate) {
            $.validator.setDefaults({
                submitHandler: function (vForm) {

                    var $form = $(vForm);
                    var formId = $form.attr('id');
                    var formUrl = $form.data('serviceurl');

                    var $submit = $form.find('[type=submit]');
                    var submitId = $submit.attr("id");
                    var serializedFieldId = "__" + submitId;
                    var $serializedField = $form.find("#" + serializedFieldId);

                    return VForms.forms[formId].events.onsubmit($serializedField.val(), formUrl, formId);
                }
            });

            // overwrite default messages
            // $.extend($.validator.messages, {
            //     required: "Override the default required message"
            // });

            $.validator.addClassRules({
                'vforms-required': {
                    required: true
                },
                'vforms-email': {
                    minlength: 6,
                    email: true
                },
                'vforms-confirmemail': {
                    minlength: 6,
                    email: true,
                    equalTo: '.vforms-email'
                },
                'vforms-password': {
                    minlength: 8
                },
                'vforms-confirmpassword': {
                    minlength: 8,
                    equalTo: '.vforms-password'
                },
                'vforms-firstname': {
                    minlength: 3
                },
                'vforms-lastname': {
                    minlength: 3
                },
                'vforms-phone': {
                    minlength: 10,
                    maxlength: 10
                },
                'vforms-title': {
                    minlength: 3
                },
                'vforms-passwordanswer': {
                    minlength: 3
                }
            });

            $('#' + element.id).validate();
        }

        if (options.transform) {
            $(current.node).jqTransform();
        }

        if (options.validate && pluginValue && VForms[pluginValue][current.node.id]) {
            var pluginClass = $(current.node).data('plugin'),
                pluginMethod = current.node.id;
            UTIL_VForms.fire(pluginClass, pluginMethod);
        }

        $('#' + element.id + ' [type=submit]').click(function (e) {

            var $submit = $(this);
            var $form = $submit.parents("form:first");
            var formId = $form.attr('id');

            VForms.common.log('form #' + formId + ' before submit');

            var formUrl = $form.data('serviceurl');
            var submitId = $submit.attr("id");
            var serializedFieldId = "__" + submitId;
            var $serializedField = $form.find("#" + serializedFieldId);

            var data = VForms.forms[formId].getData($form);

            // Serialize the from to JSON
            var jsonData = JSON.stringify(data);

            if (options.storeCookies) {

                // Save the JSON in a cookie for later
                $.cookie("formData", jsonData);

                // Save the current page scroll in a cookie
                $.cookie("formScroll", $(window).scrollTop());

                $.cookie("formId", formId);
            }

            // Put the JSON in the hidden form field
            $serializedField.val(jsonData);

            if (!options.validate) {
                e.preventDefault();
                VForms.forms[formId].events.onsubmit(jsonData, formUrl, formId);
            }
        });
    };
    var scrollToLocation = function () {
        if ($.cookie("formScroll")) {
            $(window).scrollTop($.cookie("formScroll"));
            $.cookie("formScroll", null);
        }
    };
    var loadSavedData = function () {
        if ($.cookie("formData") && $.cookie("formId")) {

            // Populate the form
            $('#' + $.cookie("formId")).populate(JSON.parse($.cookie("formData")));

            // Unset form data cookie
            $.cookie("formData", null);
            $.cookie("formId", null);
        }
    };
    var fixInputNames = function () {
        $("input:not([name]), select, textarea").attr("name", function () { return $(this).attr("id"); });
    };
    var checkForUploads = function () {
        // Find all forms with uploads
        var $formsWithUploads = $("input[type=file]").parents("form");

        // Make sure any forms with files uploads have 'multipart/form-data' enabled
        $formsWithUploads.filter(":not([enctype])").attr("enctype", "multipart/form-data");

        // Make sure any forms with files uploads have properly set their 'accept' attributes
        $formsWithUploads.filter(":not([accept])").attr("accept", "image/jpg, image/jpeg, image/png");
    };
    var appendSerializedField = function () {
        $(current.node).append(function () {

            var $form = $(this),
                $submit = $form.find("[type=submit]"),
                submitId = $submit.attr("id"),
                serializedFieldId = "__" + submitId;

            return $("<input>", {
                type: "hidden",
                name: serializedFieldId,
                id: serializedFieldId
            });
        });
    };
    var getData = function ($form) {

        var data = $form.serializeObject();
        var key;

        for (key in data) {
            // Filter out fields prefixed with "__"
            if (key.match(/^__/)) {
                delete data[key];
            }

            // Replace "on"/"off" values with true/false
            if (data[key] == "on") {
                data[key] = true;
            } else if (data[key] == "off") {
                data[key] = false;
            }
        }

        return data;
    };
    var submitHandler = function (jsonData, formUrl, formId) {

        VForms.common.log('form #' + formId + ' submitHandler');
        VForms.common.log('url: ' + formUrl);
        VForms.common.log('data: ' + jsonData);

        VForms.common.post(this, jsonData, formUrl, formId);

        return true;
    };
    var successHandler = function (data, textStatus, xhr, formId) {
        VForms.common.log('form #' + formId + ' submit success');

        var vJsonResponse = VForms.common.loadResponse(data);

        return VForms.common.handleResponse(this, vJsonResponse, formId);
    };
    var errorHandler = function (xhr, textStatus, errorThrown, formId) {
        VForms.common.log('form #' + formId + ' submit error');
        $(VForms.common.validationSelector).html('<p>An error occurred contacting the server, please try again.</p>').modal({ containerId: 'errorContainer',  overlayClose : true, overlayId: 'errorOverlay', closeHTML: '<a>X</a>'});
    };
    var completeHandler = function (xhr, textStatus, formId) {
        VForms.common.log('form #' + formId + ' submit complete');
        VForms.forms[formId].events.iswaiting = false;
    };
    // server success
    var handleSuccess = function (vJsonResponse, formId) {
        VForms.common.log('form #' + formId + ' handle success');

        var $form = $('#' + formId);
        var $target = $('#' + $form.data('targetid'));

        if (vJsonResponse.Data.Html) {
            if ($target.length) {
                $target.html(vJsonResponse.Data.Html);
            }
            else {
                $form.html(vJsonResponse.Data.Html);
            }
        }

        if (vJsonResponse.Data.gaTrackPageView) {
            // call _gaq.trackPageView(gaTrackPageView);
        }

        if (vJsonResponse.Data.JSParameters) {
            VForms.common.loadWithOptions(vJsonResponse.Data.JSParameters);
        }

        // special case for VC
        if (vJsonResponse.Data.Puzzle) {
            if (window.console) {
                console.log("puzzle init");
            }
            var position;
            var element;
            // set onvalid to temporary function to call later
            var onValid = VForms.forms['form-puzzle'].events.onvalid[0];

            $('.pieces div').draggable({
                start: function (event, ui) {
                    position = ui.originalPosition;
                },
                revert: 'invalid'
            });

            $('.puzzle .empty-piece').droppable({

                drop: function (event, ui) {

                    $('#pieceid').val(ui.draggable.data('id'));
                    element = ui.draggable;
                    $('#submit').click();
                }
            });

            var correctPiece = function (vJsonResponse, formId) {
                $('.empty-piece').css("background-image", element.css("background-image"));
                element.remove();
                // call onValid after 2 seconds so that puzzle piece can snap into place
                setTimeout(function () { return onValid(vJsonResponse, formId); }, 2000);
            };

            var wrongPiece = function (vJsonResponse, formId) {
                element.animate({ top: position.top, left: position.left });
            };

            VForms.forms['form-puzzle'].events.onvalid = correctPiece;
            VForms.forms['form-puzzle'].events.oninvalid = wrongPiece;
            VForms.forms['form-puzzle'].events.onaborted = wrongPiece;
        }

    };

    var handleFailure = function (vJsonResponse, formId) {
        VForms.common.log('form #' + formId + ' handle validation errors');

        var errorsUl = $('<ul/>');

        $.each(vJsonResponse.Properties.TaskValidationErrors, function (i, v) {
            var msg = v["Message"] || v["ErrorMessage"];
            if (msg) {
                $('<li/>', {
                    html: msg
                }).appendTo(errorsUl);
            }
        });

        $(VForms.common.validationSelector).html(errorsUl).modal({ containerId: 'errorContainer',  overlayClose : true, overlayId: 'errorOverlay', closeHTML: '<a>X</a>'});
    };
    return {
        init: init
    };
} ());

// data-init = class, use "uls"
// data-serviceurl = web service url ex. /App_Webservices/Public/WebServices.asmx/ServiceName
// TODO: data-targetid = target id for populating data
VForms.uls = (function () {
    var current;

    var init = function (args) {

        var element = args.element;
        var options = args.options;

        current = this[element.id] = {
            node: element,
            options: options,
            getData: getData,
            events: {
                onsubmit: submitHandler,
                onsuccess: [successHandler],
                onerror: [errorHandler],
                oncomplete: [completeHandler],
                onvalid: [handleSuccess],
                onfailure: [handleFailure],
                iswaiting: false
            }
        };

        VForms.common.log('ul #' + element.id + ' init');

        $('#' + element.id + ' a').click(function (e) {

            var $a = $(this),
                $ul = $a.parents('ul:first'),
                ulId = $ul.attr('id'),
                ulUrl = $ul.data('serviceurl');

            VForms.common.log('ul #' + ulId + ' before submit');

            $ul.find('a.active').removeClass('active');
            $ul.parents('li:first').not('li.results').prop('class', null);

            $a.addClass('active');

            // Serialize the from to JSON
            var jsonData = VForms.uls[ulId].getData($a);

            e.preventDefault();
            VForms.uls[ulId].events.onsubmit(jsonData, ulUrl, ulId);

        });
    };
    var getData = function ($a) {

        return JSON.stringify($a.data('id'));
    };
    var submitHandler = function (jsonData, ulUrl, ulId) {

        VForms.common.log('ul #' + ulId + ' submitHandler');
        VForms.common.log('url: ' + ulUrl);
        VForms.common.log('data: ' + jsonData);

        VForms.common.post(this, jsonData, ulUrl, ulId);

        return true;
    };
    var successHandler = function (data, textStatus, xhr, ulId) {
        VForms.common.log('ul #' + ulId + ' submit success');

        var vJsonResponse = VForms.common.loadResponse(data);

        return VForms.common.handleResponse(this, vJsonResponse, ulId);
    };
    var errorHandler = function (xhr, textStatus, errorThrown, ulId) {
        VForms.common.log('ul #' + ulId + ' submit error');
        $(VForms.common.validationSelector).html('<p>An error occurred contacting the server, please try again.</p>').modal({ containerId: 'errorContainer', overlayClose: true, overlayId: 'errorOverlay', closeHTML: '<a>X</a>' });
    };
    var completeHandler = function (xhr, textStatus, ulId) {
        VForms.common.log('ul #' + ulId + ' submit complete');
        VForms.uls[ulId].events.iswaiting = false;
    };
    // server success
    var handleSuccess = function (vJsonResponse, ulId) {
        VForms.common.log('ul #' + ulId + ' handle success');

        if (vJsonResponse.Data.Html || vJsonResponse.Data.BlurbHtml) {

            // title swaps
            var $labelList = $('#labels li');
            $labelList.prop('class', null);
            $labelList.hide();

            switch (ulId) {
                case "profiles":
                    $($labelList[4]).show();
                    $($labelList[2]).show();
                    $($labelList[1]).show();
                    $($labelList[0]).show();
                    break;
                case "roles":
                    $($labelList[3]).show();
                case "departments":
                    $($labelList[2]).show();
                case "regions":
                    $($labelList[1]).show();
                    $($labelList[0]).show();
                default:
                    break;
            }

            // ui swaps
            var $uiDir = $('.directory-ui');
            $uiDir.removeClass('step-1 step-2 step-3');

            switch (ulId) {

                case "regions":
                    $uiDir.addClass('step-2');
                    $('#department').addClass('active');
                    break;

                case "departments":
                    $uiDir.addClass('step-3');
                    $('#role').addClass('active');
                    break;

                case "roles":
                    $uiDir.addClass('step-3');
                    $('#results').addClass('active');
                    break;
                case "profiles":
                    $uiDir.addClass('step-3');
                    $('#profile').addClass('active');
                    break;

                default:
                    break;
            }

            var $li = $('#' + ulId).parents('li:first');
            //$li.prevAll().andSelf().addClass("inactive"); // and self maybe? wait for design
            $li.not('li.results').addClass("inactive");

            if (vJsonResponse.Data.Html) {
                var $after = $li.nextAll().not(':last');
                $after.each(function () {
                    $(this).remove();
                });
                $(vJsonResponse.Data.Html).insertAfter($li);
            }

            if (vJsonResponse.Data.BlurbHtml) {
                var $ul = $('#' + ulId).parents('ul:first');
                var $results = $ul.find('li.results');
                $results.html(vJsonResponse.Data.BlurbHtml);
            }
        }

        if (vJsonResponse.Data.JSParameters) {
            VForms.common.loadWithOptions(vJsonResponse.Data.JSParameters);
        }

        if (vJsonResponse.Data.gaTrackPageView) {
            // call _gaq.trackPageView(gaTrackPageView);
            // postBackHtml = "/form-submit/client-inquries/generalinquiry"
        }
    };
    var handleFailure = function (vJsonResponse, ulId) {
        VForms.common.log('ul #' + ulId + ' handle validation errors');

        var errorsUl = $('<ul/>');

        $.each(vJsonResponse.Properties.TaskValidationErrors, function (i, v) {
            var msg = v["Message"] || v["ErrorMessage"];
            if (msg) {
                $('<li/>', {
                    html: msg
                }).appendTo(errorsUl);
            }
        });

        $(VForms.common.validationSelector).html(errorsUl).modal({ containerId: 'errorContainer', overlayClose: true, overlayId: 'errorOverlay', closeHTML: '<a>X</a>' });
    };
    return {
        init: init
    };
} ());

// data-init = class, use "selects"
// data-serviceurl = web service url ex. /App_Webservices/Public/WebServices.asmx/ServiceName
// data-targetid = target id for populating data
VForms.selects = (function () {
    var current;

    var init = function (args) {

        var element = args.element;
        var options = args.options;

        current = this[element.id] = {
            node: element,
            options: options,
            getData: getData,
            events: {
                onsubmit: submitHandler,
                onsuccess: [successHandler],
                onerror: [errorHandler],
                oncomplete: [completeHandler],
                onvalid: [handleSuccess],
                onfailure: [handleFailure],
                iswaiting: false
            }
        };

        VForms.common.log('select #' + element.id + ' init');

        $('#' + element.id).change(function (e) {

            var $select = $(this),
                selectId = this.id,
                selectUrl = $select.data('serviceurl');

            VForms.common.log('select #' + selectId + ' before submit');

            // Serialize the from to JSON
            var jsonData = VForms.selects[selectId].getData($select);

            e.preventDefault();
            VForms.selects[selectId].events.onsubmit(jsonData, selectUrl, selectId);

        });
    };
    var getData = function ($select) {
        return JSON.stringify($select.val());
    };
    var submitHandler = function (jsonData, selectUrl, selectId) {

        VForms.common.log('select #' + selectId + ' submitHandler');
        VForms.common.log('url: ' + selectUrl);
        VForms.common.log('data: ' + jsonData);

        VForms.common.post(this, jsonData, selectUrl, selectId);

        return true;
    };
    var successHandler = function (data, textStatus, xhr, selectId) {
        VForms.common.log('select #' + selectId + ' submit success');

        var vJsonResponse = VForms.common.loadResponse(data);

        return VForms.common.handleResponse(this, vJsonResponse, selectId);
    };
    var errorHandler = function (xhr, textStatus, errorThrown, selectId) {
        VForms.common.log('select #' + selectId + ' submit error');
        $(VForms.common.validationSelector).html('<p>An error occurred contacting the server, please try again.</p>').modal({ containerId: 'errorContainer',  overlayClose : true, overlayId: 'errorOverlay', closeHTML: '<a>X</a>'});
    };
    var completeHandler = function (xhr, textStatus, selectId) {
        VForms.common.log('select #' + selectId + ' submit complete');
        VForms.selects[selectId].events.iswaiting = false;
    };
    // server success
    var handleSuccess = function (vJsonResponse, selectId) {
        VForms.common.log('select #' + selectId + ' handle success');

        var $select = $('#' + selectId);
        var $target = $('#' + $select.data('targetid'));

        // we expect options to be generated from the value of the last select
        if (vJsonResponse.Data.SelectValues) {
            $target.html('<option value="">Select ...</option>');
            $.each(vJsonResponse.Data.SelectValues, function (key, value) {
                $target
                        .append($('<option>', { value: key })
                        .text(value));
            });
        }

        if (vJsonResponse.Data.gaTrackPageView) {

        }
    };

    var handleFailure = function (vJsonResponse, selectId) {
        VForms.common.log('select #' + selectId + ' handle validation errors');

        var errorsUl = $('<ul/>');

        $.each(vJsonResponse.Properties.TaskValidationErrors, function (i, v) {
            var msg = v["Message"] || v["ErrorMessage"];
            if (msg) {
                $('<li/>', {
                    html: msg
                }).appendTo(errorsUl);
            }
        });

        $(VForms.common.validationSelector).html(errorsUl).modal({ containerId: 'errorContainer',  overlayClose : true, overlayId: 'errorOverlay', closeHTML: '<a>X</a>', closeHTML: '<a>X</a>'});
    };
    return {
        init: init
    };
} ());

window.UTIL_VForms = {

    fire: function (func, funcname, args) {

        var namespace = VForms;

        funcname = (funcname === undefined) ? 'init' : funcname;

        if (func !== '' && namespace[func] && typeof namespace[func][funcname] == 'function') {

            namespace[func][funcname](args);
        }

    },

    loadEvents: function (fn, options) {

        options = options || {};

        var $tags;
        // not sure if this will be useful, but may be for divs that act as forms
        // for now use one or the other ids or tags
        if (options.tags || options.ids) {
            $tags = $((options.tags ? options.tags : options.ids));
        }
        else {
            $tags = $('form');
        }

        $tags.each(function () {

            var initClass = $(this).data('init'),
                classMethod = 'init';

            UTIL_VForms.fire('common', 'init', { debug: true });
            //UTIL_VForms.fire($(this).data('plugin'));
            UTIL_VForms.fire(initClass, classMethod, { element: this, options: options });
        });
    }
};

$(document).ready(function () {
    UTIL_VForms.loadEvents($, { validate: true }); // loads forms with validation
    UTIL_VForms.loadEvents($, { tags: 'select'});
});