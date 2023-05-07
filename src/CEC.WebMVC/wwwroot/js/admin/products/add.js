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
                    .append('<img src="/' + data?.filePath + '" class="rounded img-thumbnail" alt="product image" loading="lazy" style="height:300px" >');
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