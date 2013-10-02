// Some fluff in here to give the minifyer some work to do.
(function () {

    function singleJsFile() {
        
        var element = document.getElementById("single-js-file");
        element.innerText = element.innerText.replace(" not ", " ");
        element.className = element.className + " pass";
        
    }

    singleJsFile();

}());
