var ZipCodeFiller = (function () {
    $('input.ZipCode').on('blur', function () {
        $(this).tooltip({ title: 'Seattle WA' });
    });
})();