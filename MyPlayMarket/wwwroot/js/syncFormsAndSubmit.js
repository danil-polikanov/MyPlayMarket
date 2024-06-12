
function syncFormsAndSubmit() {
    var filterForm = document.getElementById('filterForm');
    var sortForm = document.getElementById('sortForm');


    sortForm.querySelector('input[name="filterDTO.Name"]').value = filterForm.querySelector('input[name="filterDTO.Name"]').value;
    sortForm.querySelector('input[name="filterDTO.Company"]').value = filterForm.querySelector('input[name="filterDTO.Company"]').value;
    sortForm.querySelector('input[name="filterDTO.Release"]').value = filterForm.querySelector('input[name="filterDTO.Release"]').value;

    sortForm.submit();
}