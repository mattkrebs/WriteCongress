var CongressPersonFinder = function () {
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
        window.localStorage.setItem("representative", JSON.stringify(this.Representative));
        window.localStorage.setItem("senators", JSON.stringify(this.Senators));
    },
    
    Find:function(address) {
        this.FindSenators(address);
        this.FindRepresentative(address);
    },
    FindSenators:function(address) {
        var me = this;
        me.Senators = null;
        $.get('/Data/SenatorsByState', { state: address.State }).done(function(data) {
            if (data.length == 2) {
                me.Senators = data;
            }
            if (typeof me.SenatorLookupComplete === "function") {
                me.SenatorLookupComplete();
            }
            me.Save();
        });
    },
    FindRepresentative: function(address) {
        var me = this;
        me.Representative = null;

        var lookupDone = function (data) {
            if (data.length === 1) {
                me.Representative = data[0];
            } else {
                me.Representative = "needaddress";
            }
            if (typeof me.RepresentativeLookupComplete === "function") {
                me.RepresentativeLookupComplete();
            }
            me.Save();
        };

        var district = address.CongressionalDistrict;
        if ( (district!== -1 && district!==null) && address.State.length == 2) {
            $.get('/Data/RepresentativeByStateAndDistrict', { state: address.State, district: address.CongressionalDistrict },lookupDone);
        }
        else if (address.Zip.length === 5) {
            $.get('/Data/RepresentativeByZip', { zip: address.Zip }, lookupDone);
        }
    }
};