$(window).load(function() {

    var submitSearch = function() {
        var searchTerm = $('#page-search-input-box').val();
        if (searchTerm) {
            window.location = ('/facebook/search/?query=' + searchTerm);
        }
    }

    $("#page-search-button").click(function() {
        submitSearch();
    });
    $('#page-search-input-box').keydown(function(e) {
        if (e.keyCode == 13) {
            submitSearch();
        }
    });

});