var CongressPersonFinder = function (address) {
    this.Address = address || new Address();
    
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
    
    Find:function() {
        this.FindSenators();
        this.FindRepresentative();
    },
    FindSenators:function() {
        var me = this;
        me.Senators = null;
        $.get('/Data/SenatorsByState', { state: this.Address.State }).done(function(data) {
            if (data.length == 2) {
                me.Senators = data;
            }
            if (typeof me.SenatorLookupComplete === "function") {
                me.SenatorLookupComplete();
            }
            me.Save();
        });
    },
    FindRepresentative: function() {
        var me = this;
        me.Representative = null;

        var lookupDone = function (data) {
            if (data.length === 1) {
                me.Representative = data[0];
            }
            if (typeof me.RepresentativeLookupComplete === "function") {
                me.RepresentativeLookupComplete();
            }
            me.Save();
        };

        var district = this.Address.CongressionalDistrict;
        if ( (district!== -1 && district!==null) && this.Address.State.length == 2) {
            $.get('/Data/RepresentativeByStateAndDistrict', { state: this.Address.State, district: this.Address.CongressionalDistrict },lookupDone);
        }
        else if (this.Address.Zip.length === 5) {
            $.get('/Data/RepresentativeByZip', { zip: this.Zip }, lookupDone);
        }
    }
};