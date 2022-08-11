$(document).ready(function () {
    $(document).on('submit', '#signInForm', function (e) {
        e.preventDefault();
        e.stopPropagation();
        signIn();
    });

    $(document).on('click', '#dashboardLink', function (e) {
        getDashboardData();
    });

    $(document).on('click', '#viewProfileLink', function (e) {
        getProfile();
    });
    $(document).on('click', '#updateProfileLink', function (e) {
        getUpdateProfileView();
    });

    $(document).on('click', '#btnUpdateProfileForm', function (e) {
        e.preventDefault();
        e.stopPropagation();

        var firstName = $('#updateProfileForm #firstName').val();
        var lastName = $('#updateProfileForm #lastName').val();
        var email = $('#updateProfileForm #email').val();

        var list = '';

        if (!firstName || firstName == '') {
            list += '<li>Please enter your firstname</li>';
        }

        if (!lastName || lastName == '') {
            list += '<li>Please enter your lastname</li>';
        }

        if (!email || email == '') {
            list += '<li>Please enter your email</li>';
        }

        if (list != '') {
            bootboxAlertModal('<ul>' + list + '</ul>');
            return;
        }

        updateProfile(firstName, lastName, email);
    });

    $(document).on('click', '#btnChangePasswordForm', function (e) {
        e.preventDefault();
        e.stopPropagation();

        var currentPassword = $('#curPass').val();
        var newPassword = $('#newPass').val();
        var confPassword = $('#confirmPass').val();

        var list = '';

        if (!currentPassword || currentPassword == '') {
            list += '<li>Please enter your current password</li>';
        }

        if (!newPassword || newPassword == '') {
            list += '<li>Please enter new current password</li>';
        }

        if (!confPassword || confPassword == '' || newPassword != confPassword) {
            list += '<li>Password does not match</li>';
        }

        if (list != '') {
            bootboxAlertModal('<ul>' + list + '</ul>');
            return;
        }

        changePassword();
    });

    $(document).on('click', '#coursesLink', function (e) {
        getCourseList(false);
    });

    $(document).on('click', '#btnCourseModal', function (e) {

        var name = $('#courseName').val();
        var descr = $('#courseDescr').val();

        var list = '';

        if (!name || name == '') {
            list += '<li>Please enter course name</li>';
        }

        if (!descr || descr == '') {
            list += '<li>Please enter course descr</li>';
        }

        if (list != '') {
            bootboxAlertModal('<ul>' + list + '</ul>');
            return;
        }

        var id = 0;
        id = $('#btnCourseModal').data('id');

        addOrUpdateCourse(id, name, descr);
    });

    $(document).on('click', '#coursesTable .fa-edit', function (e) {
        var obj = $(this).closest('tr').find('td');
        var id = $(obj[1]).html();
        var name = $(obj[2]).html();
        var descr = $(obj[3]).html();

        $('#courseName').val(name);
        $('#courseDescr').val(descr);
        $('#btnCourseModal').data('id', id);
        $('#courseModal').modal('show');
    });

    $(document).on('click', '#coursesTable .fa-trash', function (e) {
        var obj = $(this).closest('tr').find('td');
        var id = $(obj[1]).html();
        deleteCourse(id);
    });

    $(document).on('click', '#quizzesLink', function (e) {
        getQuizzes();
    });


    $(document).on('click', '#addQuestionBtn', function (e) {
        var newQuestion = questions();
        $(this).before(newQuestion);
    });

    $(document).on('click', '.remove-question', function (e) {
        $(this).closest('.form-group').next().first().remove();
        $(this).closest('.form-group').remove();
    });

    $(document).on('click', '#addNewQuiz', function (e) {
        getCourseList(true).done(function (courseList) {
            var options = '';
            for (var i = 0; i < courseList.length; i++) {
                options += '<option value="' + courseList[i].id + '">' + courseList[i].name + '</option>';
            }
            $('#courseDDL').append(options);

            $('#quizModal').modal('show');
        });
    });

    $(document).on('click', '#btnQuizModal', function (e) {
        e.preventDefault();
        e.stopPropagation();

        var quizName = $('#quizName').val();
        var courseId = $('#courseDDL').val();

        if (quizName == null || quizName == '') {
            bootboxAlertModal('Please enter a quiz name');
            return;
        }

        if ($('.question').length == 0) {
            bootboxAlertModal('Please enter a question');
            return;
        }

        var questionSet = [];

        var isQuestionOk = true;
        var isOptionOk = true;
        var isAnswerOk = true;

        $('.question').each(function () {
            var question = $(this).val();

            if (question.length == 0) {
                isQuestionOk = false;
                return;
            }

            var optionDv = $(this).closest('.form-group.row').next();

            var options = [];

            $(optionDv).find('.option').each(function () {
                if ($(this).val().length == 0) {
                    isOptionOk = false;
                    return;
                }
                options.push($(this).val());
            });

            var answer = $(optionDv).find('.answer').val();

            if (answer == '0') {
                isAnswerOk = false;
                return;
            }

            questionSet.push({
                question: question,
                option1: options[0],
                option2: options[1],
                option3: options[2],
                option4: options[3],
                answer: answer
            });
        });

        if (!isQuestionOk) {
            bootboxAlertModal('Please enter question name');
            return;
        }

        if (!isOptionOk) {
            bootboxAlertModal('Please enter option');
            return;
        }

        if (!isAnswerOk) {
            bootboxAlertModal('Please select the answer of the question');
            return;
        }

        var id = 0;
        id = $('#btnQuizModal').data('id');

        addOrUpdateQuiz(id, quizName, courseId, questionSet);

    });

    $(document).on('click', '#quizzesTable .fa-trash', function (e) {
        var obj = $(this).closest('tr').find('td');
        var id = $(obj[1]).html();
        deleteQuiz(id);
    });

    $(document).on('click', '#takeQuizLink', function (e) {
        takeQuiz();
    });

    $(document).on('click', '#getQuestionForExam', function (e) {
        var quizId = $('#quizListDDL').val();
        getQuestionForExam(quizId);
    });

    $(document).on('click', '#submitExamPaper', function (e) {
        var courseId = $('#courseListDDL').val();
        var quizId = $('#quizListDDL').val();

        var totalQuestions = $('#totalQuestions').val();

        var examPaper = [];

        for (var i = 0; i < totalQuestions; i++) {
            var questionId = $('#question' + i).data('qid');
            var selectedOptionNo = $('input[name="questions' + i + '"]:checked').val();
            examPaper.push({
                questionId,
                selectedOptionNo
            });
        }

        submitExam(courseId, quizId, examPaper);

    });

    $(document).on('click', '#submiStaticPaper', function (e) {
        var quizId = 1;

        var totalQuestions = 5;

        var ans = ['0', '1', '4', '2', '4', '3'];
        var correct = 0;

        for (var i = 1; i <= totalQuestions; i++) {
            var selectedOptionNo = $('input[name="q' + i + '"]:checked').val();
            if (selectedOptionNo == ans[i]) {
                correct++;
            }
        }

        submitStaticExam(quizId, totalQuestions, correct);

    });

    $(document).on('click', '#register', function (e) {
        var fname = $('#fname').val();
        var lname = $('#lname').val();
        var email = $('#email').val();
        var username = $('#uname').val();
        var pass = $('#pass').val();
        var confPass = $('#confPass').val();


        var list = '';

        if (!fname || fname == '') {
            list += '<li>Please enter your firstname</li>';
        }

        if (!lname || lname == '') {
            list += '<li>Please enter your lastname</li>';
        }
        if (!email || email == '') {
            list += '<li>Please enter your email</li>';
        }
        if (!username || username == '') {
            list += '<li>Please enter your username</li>';
        }
        if (!pass || pass == '') {
            list += '<li>Please enter your password</li>';
        }

        if (!confPass || pass != confPass) {
            list += '<li>Password does not match</li>';
        }

        if (list != '') {
            bootboxAlertModal('<ul>' + list + '</ul>');
            return;
        }

        signUP(fname, lname, email, username, pass);

    });

    $(document).on('click', '#takeXMLQuizLink', function (e) {
        takeXMLQuiz();
    });

    $(document).on('click', '#submitXMLExamPaper', function (e) {
        var quizId = 2;

        var totalQuestions = $('#totalQ').val();

        var correct = 0;

        for (var i = 0; i < totalQuestions; i++) {
            var selectedOptionNo = $('input[name="q' + i + '"]:checked').val();

            var ans = $('#ans' + i).val();

            if (selectedOptionNo == ans) {
                correct++;
            }
        }

        submitStaticExam(quizId, totalQuestions, correct);

    });

    $(document).on('change', '#courseListDDL', function (e) {
        var selectedCourse = $(this).val();

        if (selectedCourse != '0') {
            $('#quizListDDL option').each(function () {
                var course = $(this).data('course')

                if (course == '0') {
                    return;
                }

                if (course == selectedCourse) {
                    $(this).css('display', '');
                } else {
                    $(this).css('display', 'none')
                }
            });
        }

    });

    getDashboardData();

})

$(document).ajaxStart(function (event) {
    openLoader();
});

function openLoader() {
    $('#LoadingModal').modal('show');
}

function closeLoader() {
    $('.modal-backdrop').last().remove();
    $('#LoadingModal').modal('dispose');
    $('#LoadingModal').hide();
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


function signIn() {
    $.ajax({
        url: AuthURLs.Login,
        type: 'POST',
        cache: false,
        data: {
            username: $('#username').val(),
            Password: $('#password').val()
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

function getProfile() {
    $.ajax({
        url: AccountURLs.GetProfile,
        type: 'GET',
        cache: false,
        data: {},
        success: function (response) {
            closeLoader();
            $('#viewProfileDiv').html(response);
        },
        error: function (err) {
            closeLoader();
            bootboxAlertModal('There are some problem. Please try again.');
        }
    });
}

function getUpdateProfileView() {
    $.ajax({
        url: AccountURLs.UpdateProfile,
        type: 'GET',
        cache: false,
        data: {},
        success: function (response) {
            closeLoader();
            $('#updateProfileDiv').html(response);
        },
        error: function (err) {
            closeLoader();
            bootboxAlertModal('There are some problem. Please try again.');
        }
    });
}

function updateProfile(firstName, lastName, email) {
    $.ajax({
        url: AccountURLs.UpdateProfile,
        type: 'POST',
        cache: false,
        data: {
            firstName,
            lastName,
            email
        },
        success: function (response) {
            closeLoader();
            if (response.success) {
                bootboxSuccessModal('Successfully updated the profile');
            } else {
                bootboxAlertModal('Failed to update the profile');
            }
        },
        error: function (err) {
            closeLoader();
            bootboxAlertModal(errorList(err.responseJSON));
        }
    });
}

function changePassword() {
    $.ajax({
        url: AccountURLs.ChangePassword,
        type: 'POST',
        cache: false,
        data: {
            currentPassword: $('#changePasswordForm #curPass').val(),
            newPassword: $('#changePasswordForm #newPass').val()
        },
        success: function (response) {
            closeLoader();
            if (response.success) {
                bootboxSuccessModal('Successfully updated the password');
            } else {
                bootboxAlertModal('Failed to update the password');
            }
        },
        error: function (err) {
            closeLoader();
            bootboxAlertModal(errorList(err.responseJSON));
        }
    });
}

function errorList(errors) {
    var list = '';

    for (var i = 0; i < errorList.length; i++) {
        list += '<li>' + errors[i].description + '</li>';
    }

    return '<ul>' + list + '</ul>';
}

function getCourseList(needToReturn) {
    var def = $.Deferred();
    $.ajax({
        url: AccountURLs.GetCourses,
        type: 'GET',
        cache: false,
        data: {},
        success: function (response) {
            closeLoader();
            if (needToReturn) {
                return def.resolve(response);
            }
            initCourseDataTable('coursesTable', response);
            return def.resolve(response);
        },
        error: function (err) {
            closeLoader();
            bootboxAlertModal(err.responseText);
            return def.resolve();
        }
    });

    return def.promise();
}

function addOrUpdateCourse(id, name, descr) {
    var url = AccountURLs.AddCourse;
    if (id != '0') {
        url = AccountURLs.UpdateCourse
    }

    $.ajax({
        url: url,
        type: 'POST',
        cache: false,
        data: {
            id: id,
            name: name,
            description: descr
        },
        success: function (response) {
            closeLoader();
            $('#courseModal').modal('hide');
            bootbox.alert({
                title: 'Success',
                message: response,
                callback: function () {
                    getCourseList(false);
                }
            });
        },
        error: function (err) {
            closeLoader();
            bootboxAlertModal(err.responseText);
        }
    });
}

function deleteCourse(id) {
    $.ajax({
        url: AccountURLs.DeleteCourse,
        type: 'DELETE',
        cache: false,
        data: {
            id: id
        },
        success: function (response) {
            closeLoader();
            bootbox.alert({
                title: 'Success',
                message: response,
                callback: function () {
                    getCourseList(false);
                }
            });
        },
        error: function (err) {
            closeLoader();
            bootboxAlertModal(err.responseText);
        }
    });
}


function initCourseDataTable(tableId, data) {
    if ($.fn.DataTable.isDataTable('#' + tableId)) {
        coursesTable.clear().destroy();
    }

    var columns = [
        {
            data: null,
            render: function (data, type, row, meta) {
                return meta.row + meta.settings._iDisplayStart + 1;
            }
        },
        { data: "id" },
        { data: "name" },
        { data: "description" },
        {
            data: null,
            render: function (data, type, row) {
                return '<i class="fa fa-edit padding-right-10" aria-hidden="true" title="Click here to edit the course"></i> <i class="fa fa-trash" aria-hidden="true" title="Click here to delete the course"></i>';
            }
        }
    ];

    coursesTable = $('#' + tableId).DataTable({
        pageLength: 7,
        orderCellsTop: true,
        paging: true,
        data: data,
        deferRender: true,
        columns: columns,
        "columnDefs": [
        ]
    });
    coursesTable.on('page.dt', function () {
        $('#' + tableId + ' tbody').animate({
            scrollTop: 0
        }, 'fast');
    });
}


function getQuizzes() {
    $.ajax({
        url: AccountURLs.GetQuizzes,
        type: 'GET',
        cache: false,
        data: {},
        success: function (response) {
            closeLoader();
            initQuizzesDataTable('quizzesTable', response);
        },
        error: function (err) {
            closeLoader();
            bootboxAlertModal(err.responseText);
        }
    });
}

function deleteQuiz(id) {
    $.ajax({
        url: AccountURLs.DeleteQuiz,
        type: 'DELETE',
        cache: false,
        data: {
            id: id
        },
        success: function (response) {
            closeLoader();
            bootbox.alert({
                title: 'Success',
                message: response,
                callback: function () {
                    getQuizzes();
                }
            });
        },
        error: function (err) {
            closeLoader();
            bootboxAlertModal(err.responseText);
        }
    });
}

function addOrUpdateQuiz(id, quizName, courseId, questionSet) {
    var url = AccountURLs.AddQuiz;
    if (id != '0') {
        url = AccountURLs.UpdateQuiz
    }

    $.ajax({
        url: url,
        type: 'POST',
        cache: false,
        data: {
            id: id,
            quizName: quizName,
            courseId: courseId,
            quesAndAns: questionSet
        },
        success: function (response) {
            closeLoader();
            $('#quizModal').modal('hide');
            bootbox.alert({
                title: 'Success',
                message: response,
                callback: function () {
                    getQuizzes();
                }
            });
        },
        error: function (err) {
            closeLoader();
            bootboxAlertModal(err.responseText);
        }
    });
}

function initQuizzesDataTable(tableId, data) {
    if ($.fn.DataTable.isDataTable('#' + tableId)) {
        quizzesTable.clear().destroy();
    }

    var columns = [
        {
            data: null,
            render: function (data, type, row, meta) {
                return meta.row + meta.settings._iDisplayStart + 1;
            }
        },
        { data: "id" },
        { data: "quizName" },
        { data: "courseId" },
        {
            data: null,
            render: function (data, type, row) {
                return '<i class="fa fa-trash" aria-hidden="true" title="Click here to delete the quiz"></i>';
            }
        }
    ];

    quizzesTable = $('#' + tableId).DataTable({
        pageLength: 7,
        orderCellsTop: true,
        paging: true,
        data: data,
        deferRender: true,
        columns: columns,
        "columnDefs": [
        ]
    });
    quizzesTable.on('page.dt', function () {
        $('#' + tableId + ' tbody').animate({
            scrollTop: 0
        }, 'fast');
    });
}

function takeQuiz() {
    $.ajax({
        url: AccountURLs.TakeQuiz,
        type: 'GET',
        cache: false,
        data: {},
        success: function (response) {
            closeLoader();
            $('#takeQuizDiv').html(response);
        },
        error: function (err) {
            closeLoader();
            bootboxAlertModal('There are some problem. Please try again.');
        }
    });
}

function getQuestionForExam(quizId) {
    $.ajax({
        url: AccountURLs.GetQuestions,
        type: 'GET',
        cache: false,
        data: { quizId },
        success: function (response) {
            closeLoader();
            $('#examPaperDiv').html(response);
        },
        error: function (err) {
            closeLoader();
            bootboxAlertModal('There are some problem. Please try again.');
        }
    });
}


function questions() {
    var ques = `<div class="form-group row">
                    <div class="col-10">
                        <input maxlength="100" class="form-control question" type="text" value="" placeholder="Enter the question">
                    </div>
                    <div class="col-2">
                         <button type="button" class="btn btn-danger remove-question">Remove Question</button>
                     </div>
                </div>
                <div class="form-group row">
                    <div class="col width-20">
                        <input maxlength="20" class="form-control option" type="text" value="" placeholder="Enter option 1">
                    </div>
                    <div class="col width-20">
                        <input maxlength="20" class="form-control option" type="text" value="" placeholder="Enter option 2">
                    </div>
                    <div class="col width-20">
                        <input maxlength="20" class="form-control option" type="text" value="" placeholder="Enter option 3">
                    </div>
                    <div class="col width-20">
                        <input maxlength="20" class="form-control option" type="text" value="" placeholder="Enter option 4">
                    </div>
                    <div class="col width-20">
                        <select class="form-control answer">
                          <option value="0">Select ans no</option>
                          <option value="1">1</option>
                          <option value="2">2</option>
                          <option value="3">3</option>
                          <option value="4">4</option>
                        </select>
                    </div>
                  </div>`;

    return ques;
}

function submitExam(courseId, quizId, examPaper) {
    $.ajax({
        url: AccountURLs.SubmitExam,
        type: 'POST',
        cache: false,
        data: {
            courseId: courseId,
            quizId: quizId,
            examPaper: examPaper
        },
        success: function (response) {
            closeLoader();
            bootbox.alert({
                title: 'Success',
                message: response,
                callback: function () {
                    takeQuiz();
                }
            });
        },
        error: function (err) {
            closeLoader();
            bootboxAlertModal(err.responseText);
        }
    });
}


function getDashboardData() {

    if (typeof AccountURLs == 'undefined') {
        return;
    }

    $.ajax({
        url: AccountURLs.GetDashboardData,
        type: 'GET',
        cache: false,
        data: {},
        success: function (response) {
            closeLoader();
            initDashboardDataTable('dashboardTable', response);
        },
        error: function (err) {
            closeLoader();
            bootboxAlertModal('There are some problem. Please try again.');
        }
    });
}

function initDashboardDataTable(tableId, data) {
    if ($.fn.DataTable.isDataTable('#' + tableId)) {
        dashboardTable.clear().destroy();
    }


    var columns = [
        {
            data: null,
            render: function (data, type, row, meta) {
                return meta.row + meta.settings._iDisplayStart + 1;
            }
        },
        { data: "userId" },
        { data: "courseName" },
        { data: "quizId" },
        { data: "score" }
    ];


    if ($('#' + tableId + ' input').length == 0) {

        // Setup - add a text input to each footer cell
        $('#' + tableId + ' thead tr').clone(true).appendTo('#' + tableId + ' thead');
        $('#' + tableId + ' thead tr:eq(1) th').each(function (i) {
            $(this).html('<input type="text" style="width: 100px" placeholder="Search" />');

            $('input', this).on('keyup change', function () {
                if (dashboardTable.column(i).search() !== this.value) {
                    dashboardTable
                        .column(i)
                        .search(this.value)
                        .draw();
                }
            });
        });

    }

    dashboardTable = $('#' + tableId).DataTable({
        pageLength: 7,
        orderCellsTop: true,
        paging: true,
        data: data,
        deferRender: true,
        columns: columns,
        "columnDefs": [
            { "searchable": true, "targets": 0 }
        ]
    });
    dashboardTable.on('page.dt', function () {
        $('#' + tableId + ' tbody').animate({
            scrollTop: 0
        }, 'fast');
    });
}

function submitStaticExam(quizId, totalQuestions, correct) {
    $.ajax({
        url: AccountURLs.SubmitStaticExam,
        type: 'POST',
        cache: false,
        data: {
            quizId: quizId,
            totalQuestions: totalQuestions,
            correct: correct
        },
        success: function (response) {
            closeLoader();
            bootbox.alert({
                title: 'Success',
                message: response,
                callback: function () {
                    $('#dashboardLink').click();
                }
            });
        },
        error: function (err) {
            closeLoader();
            bootboxAlertModal(err.responseText);
        }
    });
}

function signUP(fname, lname, email, username, pass) {
    $.ajax({
        url: AuthURLs.SignUp,
        type: 'POST',
        cache: false,
        data: {
            FirstName: fname,
            LastName: lname,
            UserName: username,
            Email: email,
            Password: pass
        },
        success: function (response) {
            closeLoader();
            bootbox.alert({
                title: 'Success',
                message: 'Registration successful',
                callback: function () {
                    window.location.replace("/auth");
                }
            });
        },
        error: function (err) {
            closeLoader();
            bootboxAlertModal(err.responseText);
        }
    });
}

function takeXMLQuiz() {
    $.ajax({
        url: AccountURLs.GetXMLQuiz,
        type: 'GET',
        cache: false,
        data: {},
        success: function (response) {
            closeLoader();
            $('#takeXMLQuizDiv').html(response);
        },
        error: function (err) {
            closeLoader();
            bootboxAlertModal('There are some problem. Please try again.');
        }
    });
}