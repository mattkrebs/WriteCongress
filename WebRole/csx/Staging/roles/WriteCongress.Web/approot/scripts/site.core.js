/// <reference path="site.Geolocator.js" />
/// <reference path="knockout-2.2.1.debug.js" />
/// <reference path="site.CongressPersonFinder.js" />


$(function () {
    ko.bindingHandlers.autoswaptext = {
        update: function (element, valueAccessor, allBindingsAccessor, viewModel) {
            var value = valueAccessor(),
                allBindings = allBindingsAccessor();
            var text = ko.utils.unwrapObservable(value);
            var placeholder = allBindings.placeholder || '';
            if (text===null || text.length == 0) {
                $(element).html(placeholder);
            } else {
                $(element).text(text);
            }
        }
    };


    var CongressPerson = function (fullNameAndTitle, openCongressId) {
        var self = this;
        this.FullNameAndTitle = ko.observable(fullNameAndTitle);
        this.Tooltip = ko.computed(function () {
            var title = self.FullNameAndTitle();
            if (title === null) {
                return "Unable to determine your Congressperson. Sometimes we need your full address (not just your zip)";
            }
            return title;
        });
        this.OpenCongressId = ko.observable(openCongressId);
        this.Photo50 = ko.computed(function () {
            if (self.OpenCongressId() != -1) {
                return 'https://az417320.vo.msecnd.net/congress-photos/' + self.OpenCongressId() + '-50px.jpg';
            } else {
                return 'https://az417320.vo.msecnd.net/congress-photos/unknown.jpg';
            }
        });
    };
    CongressPerson.Load = function (json) {
        var p = new CongressPerson(json.FullNameAndTitle, json.OpenCongressId);
        return p;
    };

    var CongressPersonDisplayViewModel = function (state, district) {
        
        var emptyPerson = new CongressPerson(null, -1);
        var self = this;
        this.State = ko.observable(state);
        this.CongressionalDistrict = ko.observable(district);
        
        this.Senators = ko.observableArray([emptyPerson,emptyPerson]);
        this.Representative = ko.observable(emptyPerson);

        this.Senators.subscribe(function () {
            self.Save();
        });
        this.Representative.subscribe(function () {
            self.Save();
        });

        this.DisplayMyCongressPersons = ko.computed(function () {
            return self.Senators()[0].OpenCongressId() != 1 && self.Senators()[1].OpenCongressId() != -1;
        });

        self.State.subscribe(function (newState) {
            if (newState == null || newState.length == 0) {
                return;
            }
            mixpanel.register({ State: newState });
            self.CongressionalDistrict(-1);
            $.get('/Data/SenatorsByState', { state: newState }).done(function(data) {
                if (data.length == 2) {
                    self.Senators([new CongressPerson(data[0].FullNameAndTitle, data[0].OpenCongressId), new CongressPerson(data[1].FullNameAndTitle, data[1].OpenCongressId)]);
                }
            });
        });

        self.CongressionalDistrict.subscribe(function (district) {
            //console.log('changing district to ' + district);finan
            if ((district !== -1 && district !== null) && self.State() != null) {
                mixpanel.register({ District: district });
                $.get('/Data/RepresentativeByStateAndDistrict', { state: self.State(), district: district }, function (data) {
                    if (data.length == 1) {
                        self.Representative(new CongressPerson(data[0].FullNameAndTitle,data[0].OpenCongressId));
                    } else {
                        mixpanel.track('Couldn\'t find a Rep for specified district', { District: district });
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
        };
    };
    CongressPersonDisplayViewModel.Load = function (info) {
        var vm = new CongressPersonDisplayViewModel(info.State, info.CongressionalDistrict);
        vm.Senators([CongressPerson.Load(info.Senators[0]), CongressPerson.Load(info.Senators[1])]);
        vm.Representative(CongressPerson.Load(info.Representative));
        return vm;
    };

    //start of the actual code execute on page load
    var myCongressionalDistrict = new CongressPersonDisplayViewModel(null, -1);
    var district = window.localStorage.getItem("myCongressionalDistrict");
    if (district != null) {
        var info = JSON.parse(district);
        myCongressionalDistrict = CongressPersonDisplayViewModel.Load(info);
    }

    if (document.getElementById('mydistrict') != null) {
        ko.applyBindings(myCongressionalDistrict, document.getElementById('mydistrict'));
    }


    wcglobals = {
        MyCongressionalDistrict:myCongressionalDistrict
    };    


    $(document).on('mouseenter', '[data-toggle="tooltip"]', function () {
        $(this).tooltip({ title: $(this).attr('title') });
        $(this).tooltip('show');
    });
    $(document).on('mouseleave', '[data-toggle="tooltip"]', function () {
        $(this).tooltip('destroy');
    });
    
    if (window.localStorage.getItem('firstview') == null) {
        window.localStorage.setItem('firstview', true);
        mixpanel.track('First View');
    }
    mixpanel.register_once({ "referrer":document.referrer,'landing page': window.location.href });
    
});