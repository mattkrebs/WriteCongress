var CheckoutModalView = function () {
    var me = this;
    this.stripeHelper = new PaymentHelper();
    this.PaymentInfo = null;
    $('#card-number').payment('formatCardNumber');

    $('#beInvolved').on('click', function () {
        me.Show();
    });

    $('#useDifferentCard').on('click', function () {
        me.UseDifferentCard();
    });

    $('#checkout').on('click', function () {
        $(this).attr('disabled', 'disabled');
        me.Checkout();
    });
};
CheckoutModalView.prototype = {
    Show: function () {
        mixpanel.track('Checking Out');
        var me = this;
        var senators = ko.toJS(wcglobals.MyCongressionalDistrict.Senators());
        var rep = ko.toJS(wcglobals.MyCongressionalDistrict.Representative());

        if (this.PaymentInfo != null) {
            $('#card-name').val(this.PaymentInfo.Name);
            $('#card-zip').val(this.PaymentInfo.Zip);
        } else {
            var name = $('#firstname').val() + ' ' + $('#lastname').val();
            $('#card-name').val(name);
            $('#card-zip').val($('#zipcode').val());
        }

        var html = '';
        if (senators.length == 2) {
            html += '<label class="checkbox"><input class="recipientSelector" type="checkbox" checked="checked" value="' + senators[0].OpenCongressId + '"><img class="photo-xsmall" src="https://az417320.vo.msecnd.net/congress-photos/' + senators[0].OpenCongressId + '-50px.jpg" /> ' + senators[0].FullNameAndTitle + '</label>';
            html += '<label class="checkbox"><input class="recipientSelector" type="checkbox" checked="checked" value="' + senators[1].OpenCongressId + '"><img class="photo-xsmall" src="https://az417320.vo.msecnd.net/congress-photos/' + senators[1].OpenCongressId + '-50px.jpg" /> ' + senators[1].FullNameAndTitle + '</label>';
        }
        if (rep != null) {
            html += '<label class="checkbox"><input class="recipientSelector" type="checkbox" checked="checked" value="' + rep.OpenCongressId + '"><img class="photo-xsmall" src="https://az417320.vo.msecnd.net/congress-photos/' + rep.OpenCongressId + '-50px.jpg" /> ' + rep.FullNameAndTitle + '</label>';
        }

        $('#congressPersonsToSendTo').html(html);

        this.showPrice(this.calculatePrice());

        $('.recipientSelector').click(function () {
            me.showPrice(me.calculatePrice());
        });

        $('#purchaseLetter').modal();
    },
    UseDifferentCard: function () {
        $('#cardOnFile').hide();
        $('#payment-form').show();
    },
    getCardInfo: function () {
        return {
            name: $('#card-name').val(),
            number: $('#card-number').val(),
            cvc: $('#card-cvc').val(),
            exp_month: $('#card-expiry-month').val(),
            exp_year: $('#card-expiry-year').val(),
            address_zip: $('#card-zip').val()
        };
    },
    validateCard: function (cardInfo) {
        $('i.error').addClass('hidden');
        $('#payment-errors').hide();
        return this.stripeHelper.ValidateCard(cardInfo);
    },
    CreateToken: function (cardInfo) {
        var deferrred = new jQuery.Deferred();
        this.stripeHelper.CreatePaymentToken(cardInfo, function (status, response) {
            if (response.error) {
                deferrred.rejectWith(this, [response.error]);
            } else {
                deferrred.resolveWith(this, [response.id]);
            }
        });
        return deferrred.promise();
    },
    Checkout: function () {
        var me = this;

        this.hideError();

        var cardInfo = this.getCardInfo();
        var errors = this.validateCard(cardInfo);

        if (errors.length > 0) {
            for (var i = 0; i < errors.length; i++) {
                var parent = $(errors[i].field).parent();
                $('i', parent).show().removeClass('hidden').tooltip({ title: errors[i].message });
            }
            $(this).removeAttr('disabled').removeClass('disabled');
            document.body.style.cursor = 'default';
            this.showError('Please correct your card information');
            return;
        }

        var promise = this.CreateToken(cardInfo);
        promise.done(function (customerId) {
            me.UpdateToken(customerId).done(function (data) {
                if (data.Success === true) {
                    me.PlaceOrder();
                } else {
                    me.showError(data.Message);
                }
            });
        });
        promise.fail(function (context, error) {
            //TODO: track this?
            me.showError(error.message);
        });
    },
    PlaceOrder: function () {
        var me = this;
        var congresspersons = $('.recipientSelector:checked');
        var personIds = [];
        for (var i = 0; i < congresspersons.length; i++) {
            personIds.push(congresspersons[i].value);
        }
        var letterSlug = $('#letterslug').val();

        
        var orderpromise = $.post('/Account/PlaceOrder', { persons: personIds.join(), letterslug: letterSlug, firstname: $("#firstname").val(), lastname: $("#lastname").val(), address1: $("#address1").val(), address2: $("#address2").val(), city: $("#city").val(), state: $("#state").val(), zipcode: $("#zipcode").val(), email: $("#email").val(), phonenumber: $("#phonenumber").val() });
        orderpromise.done(function (data) {
            if (data.Success === true) {
                mixpanel.track('Purchased Letter: Completed');
                var order = data.Data;
                window.location.href = '/Account/OrderDetail/' + order;
            } else {
                mixpanel.track('Purchased Letter: Error', { message: data.Message });
                me.showError(data.Message);
            }
        });
    },
    UpdateToken: function (token) {
        return $.post('/User/UpdatePaymentToken', { token: token });
    },
    showError: function (message) {
        $('#payment-errors').show().html(message);
        $('#checkout').removeAttr('disabled');
    },
    hideError: function (message) {
        $('#payment-errors').hide().html('');
    },
    calculatePrice: function () {
        var recipients = $('.recipientSelector:checked').length;
        var price = 1.99;
        if (recipients > 1) {
            price += (recipients - 1) * 1.49;
        }
        if (recipients == 0) {
            price = 0;
        }
        return price;
    },
    showPrice: function (price) {
        $('#checkout').html('<i class="icon-lock"></i> Checkout ($' + price.toFixed(2) + ')');
    }
};

var PaymentInfo = function () {
    this.Name = null;
    this.PaymentZip = null;
    this.CardType = null;
    this.Last4 = null;
};


var issueLetterAuthenticated = function () {
    $(function () {
        var checkoutModal = new CheckoutModalView();
        $.post('/User/GetPaymentDetails').done(function (data) {
            if (data !== false) {
                checkoutModal.PaymentInfo = new PaymentInfo();
                checkoutModal.PaymentInfo.Name = data.Name;
                checkoutModal.PaymentInfo.Last4 = data.Last4;
                checkoutModal.PaymentInfo.CardType = data.Type;
            }
            if (window.location.hash === "#sendletter") {
                checkoutModal.Show();
            }
        });
    });
}();