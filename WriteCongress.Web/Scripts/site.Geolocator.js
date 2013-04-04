/// <reference path="//ajax.googleapis.com/ajax/libs/jquery/1.9.1/jquery.min.js" />
var Address = (function (address1, address2, city, state, zip) {
	this.Address1 = address1;
	this.Address2 = address2;
	this.City = city;
	this.State = state;
	this.Zip = zip;
	this.Corrected = false;
	this.CongressionalDistrict = -1;
});

var Geolocator = (function () {
	this.zipcache = JSON.parse(window.sessionStorage.getItem("zipCodeInfoCache"));
	if (this.zipcache == null) {
		this.zipcache = [];
	}
});

Geolocator.prototype = {
    LocateMe: function () {
        var address = JSON.parse(window.localStorage.getItem("address"));
        if (address === null) {
            address = new Address();
        }
        return address;
    },
    SaveLocation:function(address) {
        window.localStorage.setItem("address", JSON.stringify(address));
        return;
    },
	CacheZipResult: function (zip,result) {
		if (typeof this.cache[zip] === "undefined" || this.cache[zip] === null) {
			this.zipcache[zip] = result;
		}
		window.sessionStorage.setItem("zipcodeInfoCache", JSON.stringify(result));
	},
	GetCachedZipResult: function (zip) {
		var deferred = new jQuery.Deferred();
		if (typeof this.zipcache[zip] !== "undefined") {
		    deferred.resolveWith(this,[this.zipcache[zip]]);
		} else {
		    deferred.resolveWith(this,[null]);
		}
	    return deferred.promise();
	},
	NormalizeAddress: function (address) {
		return $.post('/Data/NormalizedAddress', address);
	},
	GetZipCodeInfo: function (zip) {
		var deferred = new jQuery.Deferred();
		this.GetCachedZipResult(zip).done(function (info) {
			if (info !== null) {
		        deferred.resolveWith(this,[info]);
		    } else {
		        $.post('/Data/ZipCodeInfo', { zipcode: zip }, function(data) {
		            deferred.resolveWith(this,[data]);
		        });
		    }
		});
	    return deferred.promise();
	}
}