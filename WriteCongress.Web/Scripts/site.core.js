/// <reference path="site.Geolocator.js" />
/// <reference path="knockout-2.2.1.debug.js" />
/// <reference path="site.CongressPersonFinder.js" />
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
                }
            });
            self.Save();
        });

        self.CongressionalDistrict.subscribe(function (district) {
            //console.log('changing district to ' + district);finan
            if ((district !== -1 && district !== null) && self.State() != null) {
                $.get('/Data/RepresentativeByStateAndDistrict', { state: self.State(), district: district }, function (data) {
                    if (data.length == 1) {
                        self.Representative(new CongressPerson(data[0].FullNameAndTitle,data[0].OpenCongressId));
                    } else {
                        self.Representative(emptyPerson);
                    }
                });
            } else {
                self.Representative(emptyPerson);                
            }
            self.Save();
        });

        this.Save = function () {
            window.localStorage.setItem("myCongressionalDistrict", ko.toJSON(self));
        }
    };

    myCongressionalDistrict = new CongressPersonDisplayViewModel(null, -1);
    var district = window.localStorage.getItem("myCongressionalDistrict");
    if (district != null) {
        var info = JSON.parse(district);
        myCongressionalDistrict = new CongressPersonDisplayViewModel(info.State, info.CongressionalDistrict);
        myCongressionalDistrict.State(info.State);
        myCongressionalDistrict.CongressionalDistrict(info.CongressionalDistrict);
        myCongressionalDistrict.Senators(info.Senators);
    }
    ko.applyBindings(myCongressionalDistrict, document.getElementById('mydistrict'));
});