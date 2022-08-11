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
    $(document).on('click', '#reqForConsultation', function (e) {
        getConsultationForm();
    });
    $(document).on('submit', '#reqForConsultationForm', function (e) {
        openLoader();
    });
    $(document).on('click', '#addFeedbacks', function (e) {
        getFeedbackForm();
    });
    $(document).on('submit', '#addFeedbackForm', function (e) {
        e.preventDefault();
        e.stopPropagation();
        addFeedback();
    });
    $(document).on('click', '#addNewStaff', function (e) {
        getAddNewStaffForm();
    });
    $(document).on('submit', '#addNewStaffForm', function (e) {
        e.preventDefault();
        e.stopPropagation();
        addNewStaff();
    });
})

function openLoader() {
    $('#LoadingModal').modal('show');
}

function closeLoader() {
    setTimeout(function() {
        $('#LoadingModal').modal('hide');
      }, 500);
    
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
    var customerName = $('#customerName').val();
    var email = $('#email').val();
    var pass = $('#password').val();
    var companyName = $('#companyName').val();
    var contractNumber = $('#contractNumber').val();
    $.ajax({
        url: registerUrl,
        type: 'POST',
        cache: false,
        data: {
            CustomerName: customerName,
            Email: email,
            Password: pass,
            CompanyName: companyName,
            ContractNumber: contractNumber
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
function getConsultationForm() {
    openLoader();
    $.ajax({
        url: dashboardUrls.AddConsultationForm,
        type: 'GET',
        cache: false,
        data: {},
        success: function (response) {
            $('#addConsultationDiv').html(response);
            $('#consultationList').css('display', 'none');
            closeLoader();
        },
        error: function (err) {
            bootboxAlertModal('There are some problem. Please try again.');
            closeLoader();
        }
    });
}

function updateReq(reqId) {
    openLoader();
    $.ajax({
        url: dashboardUrls.UpdateConsultationForm,
        type: 'GET',
        cache: false,
        data: {
            consultationId: reqId
        },
        success: function (response) {
            $('#addConsultationDiv').html(response);
            $('#consultationList').css('display', 'none');
            closeLoader();
        },
        error: function (err) {
            bootboxAlertModal('There are some problem. Please try again.');
            closeLoader();
        }
    });
}

function getConsulationReqs() {
    window.location.replace("/dashboard/index");
}
function clickSidebar(id) {
    id = '#' + id
    $(id).click();
}

function approveOrReject(reqId, isApproved)
{
    openLoader();
    var remarks = $('#remarks').val();
    $.ajax({
        url: dashboardUrls.UpdateConsultation,
        type: 'POST',
        cache: false,
        data: {
            Id: reqId,
            Remarks: remarks,
            Status: isApproved == true ? "Approved" : "Rejected"
        },
        success: function (response) {
            closeLoader();
            bootbox.alert({
                title: 'Success',
                message: response.message,
                callback: function () {
                    window.location.replace("/dashboard/index");
                }
            });
        },
        error: function (err) {
            bootboxAlertModal(err.responseText);
            closeLoader();
        }
    });
}

function viewFile(id) {
    openLoader();
    $.ajax({
        url: dashboardUrls.GetFileUrl,
        type: 'GET',
        cache: false,
        data: {
            consultationId: id
        },
        success: function (response) {
            closeLoader();
            setTimeout(function () {
                window.open(response.url, "_blank");
            }, 500);
        },
        error: function (err) {
            bootboxAlertModal('There are some problem. Please try again.');
            closeLoader();
        }
    });
}

function getNotificaions(obj) {
    openLoader();
    $.ajax({
        url: dashboardUrls.GetNotifications,
        type: 'GET',
        cache: false,
        data: {},
        success: function (response) {
            $('#addConsultationDiv').html(response);
            $('#consultationList').css('display', 'none');
            $('.nav-link').removeClass('active');
            $(obj).addClass('active');
            closeLoader();
        },
        error: function (err) {
            bootboxAlertModal('There are some problem. Please try again.');
            closeLoader();
        }
    });
}

function getFeedbacks(obj) {
    openLoader();
    $.ajax({
        url: dashboardUrls.GetFeedbacks,
        type: 'GET',
        cache: false,
        data: {},
        success: function (response) {
            $('#addConsultationDiv').html(response);
            $('#consultationList').css('display', 'none');
            $('.nav-link').removeClass('active');
            $(obj).addClass('active');
            closeLoader();
        },
        error: function (err) {
            bootboxAlertModal('There are some problem. Please try again.');
            closeLoader();
        }
    });
}

function getFeedbackForm() {
    openLoader();
    $.ajax({
        url: dashboardUrls.AddFeedbackForm,
        type: 'GET',
        cache: false,
        data: {},
        success: function (response) {
            $('#addConsultationDiv').html(response);
            $('#consultationList').css('display', 'none');
            closeLoader();
        },
        error: function (err) {
            bootboxAlertModal('There are some problem. Please try again.');
            closeLoader();
        }
    });
}

function addFeedback() {
    openLoader();
    var customerName = $('#customerName').val();
    var companyName = $('#companyName').val();
    var serviceName = $('#serviceName').val();
    var feedbackDate = $('#feedbackDate').val();
    var feedback = $('#feedback').val();

    $.ajax({
        url: dashboardUrls.AddFeedback,
        type: 'POST',
        cache: false,
        data: {
            CustomerName: customerName,
            CompanyName: companyName,
            ServiceName: serviceName,
            FeedbackDate: feedbackDate,
            Description: feedback
        },
        success: function (response) {
            closeLoader();
            bootbox.alert({
                title: 'Success',
                message: response.message,
                callback: function () {
                    closeLoader();
                    $('#sidebarFeedback').click();
                }
            });
        },
        error: function (err) {
            bootboxAlertModal(err.responseText);
            closeLoader();
        }
    });
}

function getStaffs(obj) {
    openLoader();
    $.ajax({
        url: dashboardUrls.GetStaffs,
        type: 'GET',
        cache: false,
        data: {},
        success: function (response) {
            $('#addConsultationDiv').html(response);
            $('#consultationList').css('display', 'none');
            $('.nav-link').removeClass('active');
            $(obj).addClass('active');
            closeLoader();
        },
        error: function (err) {
            bootboxAlertModal('There are some problem. Please try again.');
            closeLoader();
        }
    });
}

function getAddNewStaffForm() {
    openLoader();
    $.ajax({
        url: dashboardUrls.AddStaff,
        type: 'GET',
        cache: false,
        data: {},
        success: function (response) {
            $('#addConsultationDiv').html(response);
            $('#consultationList').css('display', 'none');
            closeLoader();
        },
        error: function (err) {
            bootboxAlertModal('There are some problem. Please try again.');
            closeLoader();
        }
    });
}

function addNewStaff() {
    openLoader();
    var staffName = $('#staffName').val();
    var email = $('#email').val();
    var pass = $('#password').val();
    var companyName = $('#companyName').val();
    var contractNumber = $('#contractNumber').val();

    $.ajax({
        url: dashboardUrls.AddStaff,
        type: 'POST',
        cache: false,
        data: {
            Name: staffName,
            Email: email,
            Password: pass,
            CompanyName: companyName,
            ContractNumber: contractNumber
        },
        success: function (response) {
            closeLoader();
            bootbox.alert({
                title: 'Success',
                message: response.message,
                callback: function () {
                    closeLoader();
                    $('#sidebarAddNewStaff').click();
                }
            });
        },
        error: function (err) {
            bootboxAlertModal(err.responseText);
            closeLoader();
        }
    });
}