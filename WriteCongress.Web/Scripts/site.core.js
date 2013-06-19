/// <reference path="site.Geolocator.js" />
/// <reference path="knockout-2.2.1.debug.js" />
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
        html = '<img class="img-polaroid" title="Your zipcode alone isn\'t enough to determine your Senators. Try entering your full address on a letter." src="https://writecongress.blob.core.windows.net/congress-photos/unknown.jpg"/>';
    } else {
        html = '<img class="img-polaroid" title="' + data[0].FullNameAndTitle + '" src="https://writecongress.blob.core.windows.net/congress-photos/' + data[0].OpenCongressId + '-50px.jpg"/>';
        html += '<img class="img-polaroid" title="' + data[1].FullNameAndTitle + '" src="https://writecongress.blob.core.windows.net/congress-photos/' + data[1].OpenCongressId + '-50px.jpg"/>';
    }
    setSection('#mysenators', html);
}

function setMyRep(data) {
    var html = '';
    if (data === "needaddress") {
        html = '<img class="img-polaroid" title="Your zipcode alone isn\'t enough to determine your Represenative. Try entering your full address on a letter." src="https://writecongress.blob.core.windows.net/congress-photos/unknown.jpg"/>';
    } else if (data == null) {
        html = '';
    } else {
        html = '<img class="img-polaroid" title="' + data.FullNameAndTitle + '" src=""/>';
    }
    setSection('#myrep', html);
}


$(function () {
    var CongressPerson = function (fullNameAndTitle, openCongressId) {
        var self = this;
        this.FullNameAndTitle = ko.observable(fullNameAndTitle);
        this.OpenCongressId = ko.observable(openCongressId);
        this.Photo50 = ko.computed(function () {
            if (self.OpenCongressId() != -1) {
                return 'https://writecongress.blob.core.windows.net/congress-photos/' + self.OpenCongressId() + '-50px.jpg';
            } else {
                return 'https://writecongress.blob.core.windows.net/congress-photos/unknown.jpg';
            }
        });
    };

    var CongressPersonDisplayViewModel = function (state, district) {
        var emptyPerson = new CongressPerson(null, -1);
        var self = this;
        this.State = ko.observable(state);
        this.CongressionalDistrict = ko.observable(district);
        
        this.Senators = ko.observableArray([emptyPerson,emptyPerson]);
        this.Representative = ko.observable(emptyPerson);

        this.DisplayMyCongressPersons = ko.computed(function () {
            return self.Senators()[0].OpenCongressId() != 1 && self.Senators()[1].OpenCongressId() != -1;
        });

        self.State.subscribe(function () {
            self.CongressionalDistrict(-1);
            $.get('/Data/SenatorsByState', { state: self.State() }).done(function(data) {
                if (data.length == 2) {
                    self.Senators([new CongressPerson(data[0].FullNameAndTitle, data[0].OpenCongressId), new CongressPerson(data[1].FullNameAndTitle, data[1].OpenCongressId)]);
                    self.CongressionalDistrict(-1);
                }
            });
        });
        //self.CongressionalDistrict.subscribe(function (district) {
        //    if ((district !== -1 && district !== null) && self.State() != null) {
        //        $.get('/Data/RepresentativeByStateAndDistrict', { state: self.State(), district: district }, function (data) {
        //            if (data.length == 0) {
        //                self.Representative(data[0]);
        //            } else {
        //                self.Representative(emptyPerson);
        //            }
        //        });
        //    } else {
        //        self.Representative(emptyPerson);                
        //    }
        //});
    };

    myCongressionalDistrict = new CongressPersonDisplayViewModel(null, -1);
    var district = window.localStorage.getItem("congressionalDistrict");
    if (district != null) {
        var info = JSON.parse(district);
        myCongressionalDistrict = new CongressPersonDisplayViewModel(info.State, info.District);
    }
    ko.applyBindings(myCongressionalDistrict, document.getElementById('mydistrict'));
});