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
        //TODO: validate;

        $('#signup-email').val($('#email').val());
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

    $('#createAccountSendLetter').on('click', function () {
        $('#createAccountAndRedirect').submit();
    });
});