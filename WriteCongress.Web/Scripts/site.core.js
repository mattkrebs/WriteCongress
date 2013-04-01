﻿/// <reference path="site.Geolocator.js" />
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
    if (data === "needaddress") {
        html = '<img class="img-polaroid" title="Your zipcode alone isn\'t enough to determine your Represenative. Try entering your full address on a letter." src="http://writecongress.blob.core.windows.net/congress-photos/unknown.jpg"/>';
    } else if (data == null) {
        html = '';
    } else {
        html = '<img class="img-polaroid" title="' + data.FullNameAndTitle + '" src="http://writecongress.blob.core.windows.net/congress-photos/' + data.OpenCongressId + '-50px.jpg"/>';
    }
    setSection('#myrep', html);
}


$(function () {
    //some globals
    geolocator = new Geolocator();
    var address = JSON.parse(window.localStorage.getItem("address"));
    if (address === null) {
        address = new Address();
    }

    congressPersonFinder = new CongressPersonFinder(address);

    setMySenators(congressPersonFinder.Senators);
    setMyRep(congressPersonFinder.Representative);

    congressPersonFinder.RepresentativeLookupComplete = function () {
        setMyRep(congressPersonFinder.Representative);
    };
    congressPersonFinder.SenatorLookupComplete = function () {
        setMySenators(congressPersonFinder.Senators);
    };
});