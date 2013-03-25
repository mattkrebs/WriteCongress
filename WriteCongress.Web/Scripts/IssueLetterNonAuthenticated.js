$(function () {
    $('#email').on('blur', function() {
        var emailInput = $(this);
        $.post('/Authentication/CheckEmailAddress', { email: emailInput.val() }, function(data) {
            if (data.Data === true) {
                $('#signin-email').val(emailInput.val());
                $('#login-modal').modal();
                $('#signin-password').focus();
            }
        });
    });
    
    $('#signup-email').on('blur', function () {
        var emailInput = $(this);
        $.post('/Authentication/CheckEmailAddress', { email: emailInput.val() }, function (data) {
            if (data.Data === true) {
                $('i', emailInput.parent()).show().removeClass('hidden').tooltip({ title: 'This email is already in use', placement: 'bottom' });
                $('#createAccountSendLetter').attr('disabled', 'disabled');
            }
        });
    });
    $('#beInvolved').removeAttr('disabled');
    $('#beInvolved').on('click', function () {

        $('#signup-email').on('change', function () {
            validateEmail();
        });

        $('#signup-email').val($('#email').val());
        validateEmail();
        $('#signup-firstname').val($('#firstname').val());
        $('#signup-lastname').val($('#lastname').val());
        $('#signup-address1').val($('#address1').val());
        $('#signup-address2').val($('#address2').val());
        $('#signup-city').val($('#city').val());
        $('#signup-state').val($('#state').val());
        $('#signup-zipcode').val($('#zipcode').val());
        $('#signup-phonenumber').val($('#phonenumber').val());

        $('#createAccount').modal();
    });
    
    function validateEmail() {
        var emailInput = $('#signup-email');
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

                if (typeof emptyCallback === "function") {
                    emptyCallback();
                }
            }
        });
    }

    $('#createAccountSendLetter').on('click', function () {
        $('#createAccountAndRedirect').submit();
    });
});