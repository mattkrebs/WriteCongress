$(function () {
    var address = geolocator.LocateMe();
    $('#zipcode').val(address.Zip);
    $('#address1').val(address.Address1);
    $('#address2').val(address.Address2);
    $('#city').val(address.City);
    $('#state').val(address.State);
    
    $('#beInvolved').html('<i class="icon-envelope"></i> Sign Up &amp; Send!');
    $('#email').on('blur', function () {
        var emailInput = $(this);
        $.post('/Authentication/CheckEmailAddress', { email: emailInput.val() }, function (data) {
            if (data.Data === true) {
                $('#signin-email').val(emailInput.val());
                $('#login-modal').modal();
                $('#signin-password').focus();
            }
        });
    });

    $('#signup-email').on('blur', function () {
        validateEmail();
    });
    
    $('#beInvolved').removeAttr('disabled');
    $('#beInvolved').on('click', function () {
        $('#signup-email').val($('#email').val());
        $('#signup-firstname').val($('#firstname').val());
        $('#signup-lastname').val($('#lastname').val());
        $('#signup-address1').val($('#address1').val());
        $('#signup-address2').val($('#address2').val());
        $('#signup-city').val($('#city').val());
        $('#signup-state').val($('#state').val());
        $('#signup-zipcode').val($('#zipcode').val());
        $('#signup-phonenumber').val($('#phonenumber').val());
        $('#signup-congressionaldistrict').val(geolocator.CongressionalDistrict);
        
        validateEmail();
        $('#createAccount').modal();
    });

    function validateEmail() {
        var emailInput = $('#signup-email');
        $('i', emailInput.parent()).hide();
        emailInput.mailcheck({
            suggested: function (element, suggestion) {
                var suggestionParagraph = $('#emailSuggestion');
                emailInput.parent().addClass('alert alert-danger');
                suggestionParagraph.html('Did you mean <a href="javascript:void(0);" class="useEmailSuggestion" title="Use the suggestion">' + suggestion.full + '</a>?');
                suggestionParagraph.show();
                $('.useEmailSuggestion', suggestionParagraph).on('click', function () {
                    emailInput.parent().removeClass('alert-danger').removeClass('alert');
                    emailInput.val(suggestion.full);
                    suggestionParagraph.hide();
                    emailInput.change();
                });
            },
            empty: function (element, suggestion) {
                var suggestionParagraph = $('#emailSuggestion');
                suggestionParagraph.hide();

                //check to see if in use
                $.post('/Authentication/CheckEmailAddress', { email: emailInput.val() }, function (data) {
                    if (data.Data === true) {
                        $('i', emailInput.parent()).show().removeClass('hidden').tooltip({ title: 'This email is already in use', placement: 'bottom' });
                    }
                });
            }
        });
    }

    $('#createAccountSendLetter').on('click', function () {
        var button = $(this);
        button.css('cursor', 'wait');
        button.attr('disabled', 'disabled');
        var info = {
            email: $('#signup-email').val(),
            password: $('#signup-password').val(),
            passwordconfirm: $('#signup-passwordconfirm').val(),
            tos: ($('#signup-tos:checked').length == 1)
        };
        
        validateSignup(info, function(errors) {
            for (var i = 0; i < errors.length; i++) {
                $(errors[i].field).parent().addClass('alert alert-danger');
                $(errors[i].field).tooltip({ title: errors[i].message });
            }
            button.css('cursor', 'default');
            button.removeAttr('disabled');
            if (errors.length == 0) {
                $('#createAccountAndRedirect').submit();
            }
        });
    });

    var validateSignup = (function (info, callback) {
        var errors = [];
        var email = $('#signup-email').val();
        $.post('/Authentication/ValidateEmailFormat', { email: email }, function (data) {
            if (data.Success === true) {
                $.post('/Authentication/CheckEmailAddress', { email: email }, function (inuse) {
                    if (inuse.Data === true) {
                        errors.push({ field: '#signup-email', message: 'Email is already in use.' });
                    }
                    callback(errors);
                });
            } else {
                errors.push({ field: '#signup-email', message: 'Invalid email address.' });
                callback(errors);
            }
        });
        
        if (info.password.length < 4) {
            errors.push({ field: '#signup-password', message: 'Your password MUST be atleast 4 characters' });
        }

        if (info.password !== info.passwordconfirm) {
            errors.push({ field: '#signup-passwordconfirm', message: 'Your passwords should match' });
        }
        if (info.tos !== true) {
            errors.push({ field: '#signup-tos', message: 'You must agree to the Terms of Service' });
        }

    });
});