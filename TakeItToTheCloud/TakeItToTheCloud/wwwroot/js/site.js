$(document).ready(function () {
    $(document).on('submit', '#loginForm', function (e) {
        e.preventDefault();
        e.stopPropagation();
        login();
    });
    $(document).on('submit', '#registerForm', function (e) {
        e.preventDefault();
        e.stopPropagation();
        var pass = $('#password').val();
        var confPass = $('#confirmPassword').val();
        if (!confPass || pass != confPass) {
            bootboxAlertModal('Password does not match.');
            return;
        }
        register();
    });
    $(document).on('submit', '#uploadFile', function (e) {
        openLoader();
    });
})

function openLoader() {
    $('#LoadingModal').modal('show');
}

function closeLoader() {
    setTimeout(function() {
        $('#LoadingModal').modal('hide');
      }, 700);
    
}

function bootboxAlertModal(msg) {
    bootbox.alert({
        title: 'Alert',
        message: msg
    });
}

function bootboxSuccessModal(msg) {
    bootbox.alert({
        title: 'Success',
        message: msg
    });
}

function login() {
    openLoader();
    $.ajax({
        url: loginUrl,
        type: 'POST',
        cache: false,
        data: {
            email: $('#signinEmail').val(),
            Password: $('#signinPassword').val()
        },
        success: function (response) {
            closeLoader();
            window.location.replace("/" + response.redirectRoute);
        },
        error: function (err) {
            closeLoader();
            bootboxAlertModal('The login information is invalid. Please try again.');
        }
    });
}

function register() {
    openLoader();
    var firstName = $('#firstName').val();
    var lastName = $('#lastName').val();
    var email = $('#email').val();
    var pass = $('#password').val();
    var about = $('#about').val();
    $.ajax({
        url: registerUrl,
        type: 'POST',
        cache: false,
        data: {
            FirstName: firstName,
            LastName: lastName,
            Email: email,
            Password: pass,
            About: about
        },
        success: function (response) {
            closeLoader();
            bootbox.alert({
                title: 'Success',
                message: 'Registration successful',
                callback: function () {
                    window.location.replace("/account/login");
                }
            });
        },
        error: function (err) {
            bootboxAlertModal(err.responseText);
            closeLoader();
        }
    });
}
function getUploadPage(id) {
    openLoader();
    $.ajax({
        url: dashboardUrls.getUploadPage,
        type: 'GET',
        cache: false,
        data: {},
        success: function (response) {
            $('#addUploadedFileDiv').html(response);
            $('#uploadedFileList').css('display', 'none');
            $('#fileUploadId').val(id);
            closeLoader();
        },
        error: function (err) {
            bootboxAlertModal('There are some problem. Please try again.');
            closeLoader();
        }
    });
}

function deleteFile(id) {
    openLoader();
    $.ajax({
        url: dashboardUrls.deleteFile,
        type: 'POST',
        cache: false,
        data: {
            id: id
        },
        success: function (response) {
            bootboxSuccessModal(response.message);
            closeLoader();
            location.reload();
        },
        error: function (err) {
            bootboxAlertModal('There are some problem. Please try again.');
            closeLoader();
        }
    });
}

function getProfile() {
    openLoader();
    $.ajax({
        url: dashboardUrls.getProfile,
        type: 'GET',
        cache: false,
        data: {},
        success: function (response) {
            $('#addUploadedFileDiv').html(response);
            $('#uploadedFileList').css('display', 'none');
            closeLoader();
        },
        error: function (err) {
            bootboxAlertModal('There are some problem. Please try again.');
            closeLoader();
        }
    });
}