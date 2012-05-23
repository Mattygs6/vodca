/*
this is a site plugin for VForms, going to find out if it would be worth using this way
*/

VForms.vc = (function () {
    var initialized = false;
    var init = function () {
        if (!initialized) {
            console.log('virtual campus forms init');
            initialized = true;
        }
    };
    this['form-registration'] = function () {
        VForms.common.log('form-registration custom validation');

        $('#phone').rules('add', {
            minlength: 10,
            maxlength: 10,
            messages: {
                minlength: $.format('Phone number must be at least {0} characters'),
                maxlength: $.format('Phone number must be at most {0} characters')
            }
        });

        $('#email').rules('add', {
            maxlength: 63,
            messages: {
                maxlength: $.format('Email must be less than {0} characters')
            }
        });
    };
    return {
        init: init,
        'form-registration': this['form-registration']
    };
})();