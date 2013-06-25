var User = function(firstname, lastname, address1, address2, city, state, zip, email, phoneNumber, congressionalDistrict) {
    var self = this;
    this.FirstName = ko.observable(firstname);
    this.LastName = ko.observable(lastname);
    this.AddressOne = ko.observable(address1);
    this.AddressTwo = ko.observable(address2);
    this.City = ko.observable(city);
    this.State = ko.observable(state);
    this.previousState = null;

    this.Zip = ko.observable(zip);
    this.Email = ko.observable(email);
    this.PhoneNumber = ko.observable(phoneNumber);
    this.CongressionalDistrict = ko.observable(-1);

    this.ValidZip = ko.computed(function() {
        var z = self.Zip();
        return z != null && z.length == 5 && self.City() != null && self.State() != null;
    });

    //authentication related
    this.Authenticated = ko.observable(false);

    this.Email.subscribe(function(newEmail) {
        if (!self.Authenticated() && typeof wcglobals.SignIn!=="undefined") {
            $.post('/Authentication/CheckEmailAddress', { email: newEmail }, function(data) {
                if (data.Data === true) {
                    wcglobals.SignIn.Email(newEmail);
                    wcglobals.SignIn.Show();
                }
            });
        }
    });
};