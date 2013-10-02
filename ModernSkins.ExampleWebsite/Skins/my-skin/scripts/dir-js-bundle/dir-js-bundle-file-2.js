// Some fluff in here to give the minifyer some work to do.
(function () {

    function dirJsBundleFileTwo() {
        
        // Call the other function, verifying ordering
        // as well as that both files are loaded.
        window.dirJsBundleFileOne();
        
    }

    dirJsBundleFileTwo();

}());
