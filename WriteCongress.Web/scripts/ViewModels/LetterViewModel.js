var LetterViewModel = function () {
    var self = this;
    var geolocator = new Geolocator();

    //TODO: get this from something on page (like a serialized user object?);
    self.User = ko.observable(new User(null, null, null, null, null, null, null, null, null));

    self.ContinueButtonText = ko.computed(function() {
        if (self.User().Authenticated()) {
            return '<i class="icon-lock"></i> Secure Checkout';
        } else {
            return '<i class="icon-envelope"></i> Signup &amp; Mail Letter';
        }
    });

    self.User().Zip.subscribe(function (zip) {
        geolocator.GetZipCodeInfo(zip).done(function (data) {
            if (data === null) {
                mixpanel.track('Couldn\'t find info for zipcode', { Zip: zip });
            }
            self.User().City(data.City);
            self.User().State(data.StateAbbreviation);
            self.User().CongressionalDistrict(data.CongressionalDistrict);
            $('#firstname').focus();
        });
    });

    self.User().State.subscribe(function (state) {
        if (self.User().previousState != state) {
            //update the global variable
            wcglobals.MyCongressionalDistrict.State(state);

            //the state changed, we're resetting the congressional district
            if (self.User().previousState != null) {
                self.User().CongressionalDistrict(-1);
            }

            self.User().previousState = state;
        }
    });
    
    

    self.User().CongressionalDistrict.subscribe(function (district) {
        wcglobals.MyCongressionalDistrict.CongressionalDistrict(district);
    });

    var getCongressionalDistrictWithAddress = function () {
        var user = self.User();
        if (user.CongressionalDistrict() == -1) {
            if (user.AddressOne() != null && user.AddressOne().length > 4 && user.City().length > 2 && user.State().length == 2 && user.Zip().length == 5) {
                geolocator.NormalizeAddress(ko.toJS(user)).done(function (data) {
                    self.User().CongressionalDistrict(parseInt(data.CongressionalDistrict));
                });
            }
        }
    };

    self.User().AddressOne.subscribe(function () {
        getCongressionalDistrictWithAddress();
    });
    self.User().City.subscribe(function () {
        getCongressionalDistrictWithAddress();
    });

    this.Save = function () {
        localStorage.setItem("user", ko.toJSON(self.User));
    };
    
    

    this.ContinueWorkflowClick = function () {
        if (!self.User().Authenticated()) {
            if (typeof wcglobals.Signup !== "undefined") {
                mixpanel.track('Signing Up');
                wcglobals.Signup.Show();
            }
        }

    };
};