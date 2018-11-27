// отображение/скрытие оверлея
function loading(isShow) {
    if (isShow) {
        $("#loading").show();
    } else
        $("#loading").hide();
}

// создание модали
function createModal(heading, bodyContent, footerContent, modal_size) {
    var html = '<div id="modalWindow" class="modal fade" tabindex="-1" role="dialog" aria-labelledby="confirm-modal" aria-hidden="true">';
    if (modal_size != undefined && modal_size == 'sm' || modal_size == 'lg') {
        html += '<div class="modal-dialog modal-' + modal_size + '">';
    } else {
        html += '<div class="modal-dialog">';
    }

    html += '<div class="modal-content">';
    html += '<div class="modal-header">';
    html += '<a class="close" data-dismiss="modal">×</a>';
    html += '<h4 class="text-primary">' + heading + '</h4>';
    html += '</div>';
    html += '<div class="modal-body">';
    html += bodyContent;
    html += '</div>';
    html += '<div class="modal-footer">';
    html += footerContent;
    html += '</div>';  // footer
    html += '</div>';  // content
    html += '</div>';  // dialog
    html += '</div>';  // modalWindow

    $('#modalWindow').remove();
    $('.modal-backdrop').remove();
    $('body').removeClass('modal-open');

    $('body').append(html);

    $('#modalWindow').on('hide.bs.modal', function (e) {
        $(this).remove();
        $('.modal-backdrop').remove();
        $('body').removeClass('modal-open');

    });


    $("#modalWindow").modal('show');


}
