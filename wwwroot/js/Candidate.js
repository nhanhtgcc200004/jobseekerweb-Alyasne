var downloadbutton = document.getElementById("download");
downloadbutton.addEventListener("click", function () {
    var profile_user_id = downloadbutton.getAttribute("data-user_id");
    $.ajax({
        url: "/Profile/SomethingDummy",
        method: "Post",
        data: {
            user_profile_id: profile_user_id
        },
        success: function () {
            window.location.href = "/Profile/DownloadUserCv?user_profile_id=" + profile_user_id;
        },
        error: function () {
            console.log("something wrong")
        }
    })
})
var uploadButton = document.getElementById("UploadCv");
uploadButton.addEventListener("click", function () {
    var input = document.getElementById("Resume");
    const new_resume = input.files[0];
    if (new_resume) {
        var formData = new FormData();
        formData.append("Resume", new_resume);
        $.ajax({
            url: "/Profile/UploadCV",
            method: "Post",
            data: formData,
            processData: false,
            contentType: false,
            success: function () {
                window.location.reload();
            },
            error: function () {
                console.log("something wrong");
            }
        })

    }

})