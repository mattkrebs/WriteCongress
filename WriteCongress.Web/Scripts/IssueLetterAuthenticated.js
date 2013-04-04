var issueLetterAuthenticated = function() {
    $(function() {
        var stripeHelper = new PaymentHelper();
        $.post('/User/GetLetterPrefill', function(user) {
            if (user !== null) {
                $('#email').val(user.Email);
                $('#firstname').val(user.FirstName);
                $('#lastname').val(user.LastName).change();
                $('#address1').val(user.AddressOne);
                $('#address2').val(user.AddressTwo);
                $('#city').val(user.City);
                $('#state').val(user.State);
                $('#zipcode').val(user.ZipCode).change();
                $('#phonenumber').val(user.PhoneNumber);
                $('#beInvolved').removeAttr('disabled');
            }

            if (window.location.hash === "#sendletter") {
                $('#purchaseLetter').modal();
            }

            $('#card-name').val($('#firstname').val() + ' ' + $('#lastname').val());
            $('#card-zip').val($('#zipcode').val());
            CalculateAndDisplayPrice();
        });

        $.post('/User/GetPaymentDetails', function (data) {
            if (data != null) {
                $('#payment-form').hide();
                $('#card-on-file-name').val(data.Name);
                $('#card-on-file-last4').val(data.Last4);
                $('#card-on-file-img').attr('src', 'https://checkout.stripe.com/assets/cards/' + data.Type.toLowerCase() + '.png');
                $('#cardOnFile').show();
            }
        });

        $('#beInvolved').on('click', function () {
            var senators = congressPersonFinder.Senators;
            var rep = congressPersonFinder.Representative;
            var html = '';
            if(senators.length==2){
                html += '<label class="checkbox"><input class="recipientSelector" type="checkbox" checked="checked" value="' + senators[0].OpenCongressId + '"><img class="photo-xsmall" src="https://writecongress.blob.core.windows.net/congress-photos/' + senators[0].OpenCongressId + '-50px.jpg" /> ' + senators[0].FullNameAndTitle + '</label>';
                html += '<label class="checkbox"><input class="recipientSelector" type="checkbox" checked="checked" value="'+senators[1].OpenCongressId+'"><img class="photo-xsmall" src="https://writecongress.blob.core.windows.net/congress-photos/'+senators[1].OpenCongressId+'-50px.jpg" /> '+ senators[1].FullNameAndTitle+'</label>';
            }
            if (rep != null) {
                html += '<label class="checkbox"><input class="recipientSelector" type="checkbox" checked="checked" value="'+rep.OpenCongressId+'"><img class="photo-xsmall" src="https://writecongress.blob.core.windows.net/congress-photos/'+rep.OpenCongressId+'-50px.jpg" /> '+rep.FullNameAndTitle+'</label>';
            }
            $('#congressPersonsToSendTo').html(html);
            
            $('.recipientSelector').click(function() {
                CalculateAndDisplayPrice();
            });
                            
            $('#purchaseLetter').modal();
        });

        $('#useDifferentCard').on('click', function() {
            $('#cardOnFile').hide();
            $('#payment-form').show();
        });

        $('#checkout').on('click', function() {
            $('i.error').addClass('hidden');
            $('#payment-errors').hide();
            var cardInfo = {
                name: $('#card-name').val(),
                number: $('#card-number').val(),
                cvc: $('#card-cvc').val(),
                exp_month: $('#card-expiry-month').val(),
                exp_year: $('#card-expiry-year').val(),
                address_zip: $('#card-zip').val()
            };

            var errors = stripeHelper.ValidateCard(cardInfo);
            if (errors.length > 0) {
                for (var i = 0; i < errors.length; i++) {
                    var parent = $(errors[i].field).parent();
                    $('i', parent).show().removeClass('hidden').tooltip({ title: errors[i].message });
                }
                $(this).removeAttr('disabled').removeClass('disabled');
                document.body.style.cursor = 'default';
            } else {
                stripeHelper.CreatePaymentToken(cardInfo, function(status, response) {
                    if (response.error) {
                        $('#payment-errors').show().html(response.error.message);
                    } else {
                        stripeHelper.UpdateCustomerToken(response.id, function(data) {
                            if (data === true) {
                                alert('should process the order and queue the charge. customer has been saved');
                            } else {
                                $('#payment-errors').show().html(data);
                                $(this).removeAttr('disabled').removeClass('disabled');
                                document.body.style.cursor = 'default';
                            }
                        });
                    }
                });
            }
        });
    });
}();

function CalculateAndDisplayPrice() {
    var recipients = $('.recipientSelector:checked').length;
    var price = 1.99;
    if (recipients > 1) {
        price += (recipients - 1) * 1.49;
    }
    if (recipients == 0) {
        price = 0;
    }

    $('#checkout').html('<i class="icon-lock"></i> Checkout ($' + price.toFixed(2) + ')');
}