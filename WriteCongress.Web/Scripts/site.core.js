var setSection = function (selector, html) {
    var section = $(selector);
    section.html(html);
    $('img', section).tooltip({ placement: 'bottom' });
};

function setMySenators(useCache, zip) {
    if (useCache && localStorage.getItem('mysenators') !== null) {
        var html = localStorage.getItem('mysenators');
        setSection('#mysenators', html);
    } else {
        if (!useCache) {
            $.post('/Data/GetSenatorsByZip', { zip: zip }, function (data) {
                var html = '';
                if (data.length != 2) {
                    html = '<img class="img-polaroid" title="Your zipcode alone isn\'t enough to determine your Senators. Try entering your full address." src="http://writecongress.blob.core.windows.net/congress-photos/unknown.jpg"/>';
                } else {
                    html = '<img class="img-polaroid" title="' + data[0].FullNameAndTitle + '" src="http://writecongress.blob.core.windows.net/congress-photos/' + data[0].OpenCongressId + '-50px.jpg"/>';
                    html += '<img class="img-polaroid" title="' + data[1].FullNameAndTitle + '" src="http://writecongress.blob.core.windows.net/congress-photos/' + data[1].OpenCongressId + '-50px.jpg"/>';
                }
                localStorage.setItem('mysenators', html);
                setMySenators(true);
            });
        }
    }
}

function setMyRep(useCache, zip) {
    if (useCache && localStorage.getItem('myrep') !== null) {
        var html = localStorage.getItem('myrep');
        setSection('#myrep', html);
    } else {
        if (!useCache) {
            $.post('/Data/GetCongressionalDistrictByZip', { zip: zip }, function (data) {
                var html = '';
                if (data.length != 1) {
                    html = '<img class="img-polaroid" title="Your zipcode alone isn\'t enough to determine your Represenative. Try entering your full address." src="http://writecongress.blob.core.windows.net/congress-photos/unknown.jpg"/>';
                } else {
                    html = '<img class="img-polaroid" title="' + data[0].FullNameAndTitle + '" src="http://writecongress.blob.core.windows.net/congress-photos/' + data[0].OpenCongressId + '-50px.jpg"/>';
                }
                localStorage.setItem('myrep', html);
                setMyRep(true);
            });
        }
    }
}

$(function () {
    setMyRep(true);
    setMySenators(true);
});