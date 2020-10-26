$('#btnNuevo').click(function (eve) {
    $("#modal-content").load("Artist/Add");
});

$('#btnEditar').click(function (eve) {
    $('#modal-content').load('Edit');
});