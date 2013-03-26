$(function () {
    window.CongressPersonFinder = new CongressPersonFinder(null, null, null, null);
});
var CongressPersonFinder = function (address, city, state, zip) {
    this.Address = address || '';
    this.City = city || '';
    this.State = state || '';
    this.Zip = zip || '';

    this.Senators = null;
    this.SenatorLookupComplete = null;
    this.Representative = null;
    this.RepresentativeLookupComplete = null;
    
    var storageSenators = window.localStorage.getItem("senators");
    if (storageSenators != null) {
        this.Senators = JSON.parse(storageSenators);
    }
    var storageRep = window.localStorage.getItem("representative");
    if (storageRep != null) {
        this.Representative = JSON.parse(storageRep);
    }
};
CongressPersonFinder.prototype = {
    Save: function () {
        if (this.Representative !== null) {
            window.localStorage.setItem("representative", JSON.stringifY(this.Representative));
        }
        if (this.Senators !== null) {
            window.localStorage.setItem("senators", JSON.stringifY(this.Senators));
        }
    },
    Find: function (force) {
        var me = this;
        if (typeof force === "undefined") {
            force = false;
        }
        if (!force && this.Senators != null && this.Representative != null) {
            if (typeof this.SenatorLookupComplete === "function") {
                this.SenatorLookupComplete();
            }
            if (typeof this.RepresentativeLookupComplete === "function") {
                this.RepresentativeLookupComplete();
            }
        }

        //if we're forcing OR the rep is null
        if (force || this.Representative == null) {
            //logic to get reps
            if (this.Address.length > 4 && ((this.City.length > 2 && this.State.length == 2) || this.Zip.length === 5)) {
                $.post('/Data/GetCongressionalDistrictByAddress', {address:this.Address,city:this.City,state:this.State,zip: this.Zip }, function(data) {
                    if (data.length == 1) {
                        me.Representative = data[0];
                    }
                    if (typeof me.RepresentativeLookupComplete === "function") {
                        me.RepresentativeLookupComplete();
                    }
                });
                if (typeof this.RepresentativeLookupComplete === "function") {
                    this.RepresentativeLookupComplete();
                }
            } else if (this.Zip.length === 5) {
                $.post('/Data/GetCongressionalDistrictByZip', { zip: this.Zip }, function(data) {
                    if (data.length == 1) {
                        me.Representative = data[0];
                    }
                    if (typeof me.RepresentativeLookupComplete === "function") {
                        me.RepresentativeLookupComplete();
                    }
                });
            }
        }

        //if we're forcing OR the senator is null
        if (force || this.Senators == null) {
            //lookup senator
            if (this.Address.length > 4 && ((this.City.length > 2 && this.State.length == 2) || this.Zip.length === 5)) {
                //we can typically get the senator right with just a state or zip
                $.post('/Data/GetSenatorsByAddress', {address:this.Address,city:this.City,state:this.State,zip: this.Zip }, function (data) {
                    if (data.length == 2) {
                        me.Senators = data;
                    }
                    if (typeof me.SenatorLookupComplete === "function") {
                        me.SenatorLookupComplete();
                    }
                });
            } else if (this.State.length == 2 || this.Zip.length === 5) {
                //we can typically get the senator right with just a state or zip
                $.post('/Data/GetSenatorsByZip', { zip: this.Zip }, function (data) {
                    if (data.length == 2) {
                        me.Senators = data;
                    }
                    if (typeof me.SenatorLookupComplete === "function") {
                        me.SenatorLookupComplete();
                    }
                });
                
            }
        }
    }
};