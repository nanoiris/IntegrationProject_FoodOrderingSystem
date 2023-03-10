window.previewImage = (inputElem, imgElem) => {
    //const url = URL.createObjectURL(inputElem.files[0]);
    //imgElem.addEventListener('load', () => URL.revokeObjectURL(url), { once: true });
    //imgElem.src = url;
    var fileData = inputElem.files[0];
    var reader = new FileReader();
    reader.readAsDataURL(fileData);
    reader.onload = function () {
        imgElem.setAttribute("src", this.result);
    }
}

//window.initMap=(mapElement, latitude, longitude)=> {
//    // Create a new map centered at the specified coordinates
//    var map = new google.maps.Map(mapElement, {
//        center: { lat: latitude, lng: longitude },
//        zoom: 8
//    });

//    // Add a marker at the specified coordinates
//    var marker = new google.maps.Marker({
//        position: { lat: latitude, lng: longitude },
//        map: map,
//        title: 'Location'
//    });
//}

//window.initMap = (latitude, longitude) => {
//    console.log("latitude" + latitude);
//    console.log("longtitude" + longitude);
//    // Create a new map centered at the specified coordinates
//    var map = new google.maps.Map(document.getElementById('map'), {
//        center: { lat: latitude, lng: longitude },
//        zoom: 8
//    });
//    console.log("into");
//    // Add a marker at the specified coordinates
//    var marker = new google.maps.Marker({
//        position: { lat: latitude, lng: longitude },
//        map: map,
//        title: 'Location'
//    });
//    console.log("finish")
//}

window.initMap = (latitudeRest, longitudeRest, latitudeUser, longitudeUser,) => {
    // Create a new map centered at the specified coordinates
    var map = new google.maps.Map(document.getElementById('map'), {
        center: { lat: latitudeRest, lng: longitudeRest },
        zoom: 8
    });

    // Add a marker at the specified coordinates
    var marker = new google.maps.Marker({
        position: { lat: latitudeRest, lng: longitudeRest },
        map: map,
        title: 'Restaurant'
    });
    console.log("latitude" + latitudeRest);
    console.log("longtitude" + longitudeRest);
    var marker1 = new google.maps.Marker({
        position: { lat: latitudeUser, lng: longitudeUser },
        map: map,
        title: 'Customer'
    });
    console.log("latitude" + latitudeUser);
    console.log("longtitude" + longitudeUser);
}

window.closeModal = (closeId) => {
    document.getElementById(closeId).click();
}

