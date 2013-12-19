var PaymentHelper = function () {
    if (typeof Stripe === "undefined" || Stripe == null) {
        alert('Stripe.js is required on this page.');
    }
};
PaymentHelper.prototype = {
    ValidateCardNumber: function (number) {
        return Stripe.validateCardNumber(number);
    },
    ValidateExpiration: function (month, year) {
        return Stripe.validateExpiry(month, year);
    },
    ValidateCVC: function (cvc) {
        return Stripe.validateCVC(cvc);
    },
    GetCardType: function (cardNumber) {
        return Stripe.cardType(cardNumber);
    },
    ValidateCard: function (cardInfo) {
        var errors = [];
        if (!this.ValidateCardNumber(cardInfo.number)) {
            errors.push({ field: '#card-number', message: 'Please enter a valid card number' });
        }
        if (!this.ValidateCVC(cardInfo.cvc)) {
            errors.push({ field: '#card-cvc', message: 'Please enter a valid CVC' });
        }
        if (!this.ValidateExpiration(cardInfo.exp_month, cardInfo.exp_year)) {
            errors.push({ field: '#card-expiry-month', message: 'Please enter valid expiration date' });
            errors.push({ field: '#card-expiry-year', message: 'Please enter valid expiration date' });
        }
        if (cardInfo.address_zip.toString().length != 5) {
            errors.push({ field: '#card-zip', message: 'Please enter a valid zip' });
        }

        return errors;
    },
    CreatePaymentToken: function (cardInfo, stripeCallback) {
        Stripe.createToken(cardInfo, stripeCallback);
    },
    UpdateCustomerToken: function (stripeToken,callback) {
        $.post('/User/UpdatePaymentToken', { token: stripeToken}, function (data) {
            if (typeof callback === "function") {
                callback(data);
            }
        });
    }
};