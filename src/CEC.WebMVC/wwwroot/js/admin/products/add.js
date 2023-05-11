$('#formImageUpload').submit(function (e) {
    e.preventDefault();
    console.log(this.action);
    $.ajax({
        url: this.action,
        type: this.method,
        data: new FormData(this),
        cache: false,
        contentType: false,
        processData: false,
        success: function (data) {
            if (data?.success) {
                $('#product-thumbnail-el').empty()
                    .append('<img src="' + data?.filePath + '" class="rounded img-thumbnail" alt="product image" loading="lazy" style="height:300px" >');
                $('#ThumbnailUrl').val(data?.filePath);
            } else {
                $('#product-thumbnail-el').empty();
                alert(data?.errors);
            }
        },
        error: function (xhr, error, status) {
            console.error(error, status);
        }
    })
});

//Handle Camera
{
    let camera_button = document.querySelector("#start-camera");
    let video = document.querySelector("#video");
    let click_button = document.querySelector("#click-photo");
    let canvas = document.querySelector("#canvas");
    let camera_section = document.querySelector("#cameraSection");
    var localstream;

    camera_button.addEventListener('click', async function () {
        camera_section.removeAttribute("hidden");
        if (localstream?.active) {
            vidOff();
            click_button.setAttribute("disabled", "");
            camera_button.textContent = "From Camera";
            camera_button.classList.remove("btn-danger");
        } else {
            localstream = await navigator.mediaDevices.getUserMedia({ video: true, audio: false });
            video.srcObject = localstream;
            click_button.removeAttribute("disabled");
            camera_button.textContent = "Stop Camera";
            camera_button.classList.add("btn-danger");
        }
    });

    click_button.addEventListener('click', function () {
        let image_base64 = capture();
        $('#product-thumbnail-el').empty().append('<img src="' + image_base64 + '" class="rounded img-thumbnail" alt="product image" loading="lazy" style="height:300px" >');

        $('#ThumbnailUrl').val(image_base64);
    });

    function vidOff() {
        video.pause();
        video.src = "";
        localstream.getTracks().forEach(function (track) {
            track.stop();
        });
        console.log("Vid off");
    }

    function capture() {
        canvas.width = video.videoWidth;
        canvas.height = video.videoHeight;
        canvas
            .getContext("2d")
            .drawImage(video, 0, 0, video.videoWidth, video.videoHeight);
        let image_data_url = canvas.toDataURL("image/jpeg");
        return image_data_url;

        /** Code to merge image **/
        /** For instance, if I want to merge a play image on center of existing image **/
        //const playImage = new Image();
        //playImage.src = "path to image asset";
        //playImage.onload = () => {
        //    const startX = video.videoWidth / 2 - playImage.width / 2;
        //    const startY = video.videoHeight / 2 - playImage.height / 2;
        //    canvas
        //        .getContext("2d")
        //        .drawImage(playImage, startX, startY, playImage.width, playImage.height);
        //    canvas.toBlob() = (blob) => {
        //        const img = new Image();
        //        img.src = window.URL.createObjectUrl(blob);
        //    };
        //};
        /** End **/
    }
}