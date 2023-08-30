
// css reload functions
var DeveloperTool = {
    Init: function () {
        this.headObj =
            document.getElementsByTagName('html')[0].getElementsByTagName('head')[0];
        return this;
    },
    ReloadAllCSS: function (headObj) {
        console.log("DT:ReloadAllCSS");
        var links = headObj.getElementsByTagName('link');
        for (var i = 0; i < links.length; i++) {
            var link = links[i];
            this.ReloadCSSLink(link);
        }
        return this;
    },
    ReloadCSSLink: function (item) {
        var value = item.getAttribute('href');
        var cutI = value.lastIndexOf('?');
        if (cutI != -1)
            value = value.substring(0, cutI);
        item.setAttribute('href', value + '?t=' + new Date().valueOf());
        return this;
    },
    ReloadAllCSSThisPage: function () {
        this.ReloadAllCSS(this.headObj);
        return this;
    }
};

// popup functions

$(document).ready(function () {

	$('.image-popup-vertical-fit').magnificPopup({
		type: 'image',
		closeOnContentClick: true,
		mainClass: 'mfp-img-mobile',
		image: {
			verticalFit: true
		}

	});

	$('.image-popup-fit-width').magnificPopup({
		type: 'image',
		closeOnContentClick: true,
		image: {
			verticalFit: false
		}
	});

	$('.image-popup-no-margins').magnificPopup({
		type: 'image',
		closeOnContentClick: true,
		closeBtnInside: false,
		fixedContentPos: true,
		mainClass: 'mfp-no-margins', // class to remove default margin from left and right side
		image: {
			verticalFit: true
		},
		zoom: {
			enabled: false,
			duration: 1000 // don't foget to change the duration also in CSS
		},
	});

});
