// Some fluff in here to give the minifyer some work to do.
(function () {

    function dirJsBundleFileOne() {
        window.dirJsBundleFileOne = function() {
            var element = document.getElementById("dir-js-bundle");
            element.innerText = element.innerText.replace(" not ", " ");
            element.className = element.className + " pass";
        };
    }

    dirJsBundleFileOne();

}());
