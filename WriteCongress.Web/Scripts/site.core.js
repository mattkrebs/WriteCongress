/// <reference path="site.CongressPersonFinder.js" />
var setSection = function (selector, html) {
    var section = $(selector);
    section.html(html);
    $('img', section).tooltip({ placement: 'bottom' });
};


function setMySenators(data) {
    var html = '';
    if (data == null) {
        return;
    }
    if (data.length != 2) {
        html = '<img class="img-polaroid" title="Your zipcode alone isn\'t enough to determine your Senators. Try entering your full address on a letter." src="http://writecongress.blob.core.windows.net/congress-photos/unknown.jpg"/>';
    } else {
        html = '<img class="img-polaroid" title="' + data[0].FullNameAndTitle + '" src="http://writecongress.blob.core.windows.net/congress-photos/' + data[0].OpenCongressId + '-50px.jpg"/>';
        html += '<img class="img-polaroid" title="' + data[1].FullNameAndTitle + '" src="http://writecongress.blob.core.windows.net/congress-photos/' + data[1].OpenCongressId + '-50px.jpg"/>';
    }
    setSection('#mysenators', html);
}

function setMyRep(data) {
    var html = '';
    if (data == null) {
        return;
    }
    if (data instanceof Array) {
        html = '<img class="img-polaroid" title="Your zipcode alone isn\'t enough to determine your Represenative. Try entering your full address on a letter." src="http://writecongress.blob.core.windows.net/congress-photos/unknown.jpg"/>';
    } else {
        html = '<img class="img-polaroid" title="' + data.FullNameAndTitle + '" src="http://writecongress.blob.core.windows.net/congress-photos/' + data.OpenCongressId + '-50px.jpg"/>';
    }
    setSection('#myrep', html);
}

$(function () {
    var personFinder = new CongressPersonFinder(null, null, null, null);
    window.personFinder = personFinder;
    
    setMySenators(window.personFinder.Senators);
    setMyRep(window.personFinder.Representative);

    window.personFinder.RepresentativeLookupComplete = function () {
        setMyRep(window.personFinder.Representative);
    };
    window.personFinder.SenatorLookupComplete = function () {
        setMySenators(window.personFinder.Senators);
    };
    
    $('input, textarea').placeholder();
});