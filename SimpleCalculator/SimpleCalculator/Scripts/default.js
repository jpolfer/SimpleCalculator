$(document).ready(function () {
    $('.execute-button').mouseover(function () {
        $('.execute-button').val('Execute');
    });
    $('.execute-button').mouseout(function () {
        $('.execute-button').val('=');
    });
});
