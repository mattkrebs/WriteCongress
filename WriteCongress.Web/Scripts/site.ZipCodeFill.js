var ZipCodeFiller = (function () {
    $('input.ZipCode').on('blur', function () {
        var input = $(this);
        $.post('/Data/ZipCodeInfo', { zipcode: $(this).val() }, function (data) {
            if (data !== null) {
                input.tooltip({ title: data.City + ' ' + data.StateAbbreviation });
                $('.zipautofill-city').val(data.City);
                $('.zipautofill-state').val(data.StateAbbreviation);
            }
        });
    });
})();