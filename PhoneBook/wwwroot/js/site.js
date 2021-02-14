// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your Javascript code.

function confirmDelete(entryId, isDeleteClicked) {
    var deleteEditSpan = 'deleteEditSpan_' + entryId;
    var confirmDeleteSpan = 'confirmDeleteSpan_' + entryId;

    if (isDeleteClicked) {
        $('#' + deleteEditSpan).hide();
        $('#' + confirmDeleteSpan).css('display', 'flex');
    } else {
        $('#' + deleteEditSpan).show();
        $('#' + confirmDeleteSpan).hide();
    }
}