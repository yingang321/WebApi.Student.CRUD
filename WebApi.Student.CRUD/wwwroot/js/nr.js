function getQueryValue(value) {
    var query = window.location.search.substring(1);
    var queryPro = query.split("&");
    for (var i = 0; i < queryPro.length; i++) {
        var pair = queryPro[i].split("=");
        if (pair[0] == value) {
            return pair[1];
        }
    }
}