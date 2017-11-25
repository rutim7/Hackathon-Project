function UploadImage(id) {
    var uploadImageValue = $(id).val();
    $.ajax({
        type: 'POST',
        url: '/Image/UploadImage',
        data: { 'imageContent': uploadImageValue },
        dataType: 'html',
        success: function (data) {
            $("#ProfileContent").html(data);
            $('#UploadUserProfileImageContainer').empty();
            $('#UploadUserProfileImageModal.modal.inmodal.fade').modal('hide');
            $('.modal-backdrop').remove();
            $('body').removeClass('modal-open');
            $.ajax({
                type: 'GET',
                url: '/Account/GetUserInformation',
                dataType: 'html',
                success: function (dataSecond) {
                    $("#UserInformation").html(dataSecond);
                }
            });
        },
        error: function (error, message) {
            if (error.status === 400) {
                $('#ErrorMessage').text(error.statusText); // display the error message in the span tag
            }
        }
    });
}

