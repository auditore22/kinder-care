// ========================== Avatar Upload Script ===========================
function uploadImageFunction(imageId, previewId) {
    $(imageId).on('change', function () {
        var input = this;
        if (input.files && input.files[0]) {
            var reader = new FileReader();
            reader.onload = function(e) {
                $(previewId).css('background-image', 'url(' + e.target.result + ')');
                $(previewId).hide();
                $(previewId).fadeIn(650);
            }
            reader.readAsDataURL(input.files[0]);
        }
    });
}

uploadImageFunction('#coverImageUpload', '#coverImagePreview');
uploadImageFunction('#imageUpload', '#profileImagePreview');