function syncFormsAndSubmit() {
    var filterForm = document.getElementById('filterForm');
    var sortForm = document.getElementById('sortForm');

    sortForm.querySelector('input[name="filterDTO.Name"]').value = filterForm.querySelector('input[name="filterDTO.Name"]').value;
    sortForm.querySelector('input[name="filterDTO.Company"]').value = filterForm.querySelector('input[name="filterDTO.Company"]').value;


    var selectedGenres = Array.from(filterForm.querySelector('select[name="filterDTO.SelectedGenres"]').selectedOptions)
        .map(option => parseInt(option.value, 10));
    updateHiddenInputs(sortForm, "filterDTO.SelectedGenres", selectedGenres);

    var selectedTags = Array.from(filterForm.querySelector('select[name="filterDTO.SelectedTags"]').selectedOptions)
        .map(option => parseInt(option.value, 10));
    updateHiddenInputs(sortForm, "filterDTO.SelectedTags", selectedTags);


    var selectedPlatforms = Array.from(filterForm.querySelector('select[name="filterDTO.SelectedPlatforms"]').selectedOptions)
        .map(option => parseInt(option.value, 10));
    updateHiddenInputs(sortForm, "filterDTO.SelectedPlatforms", selectedPlatforms);


    sortForm.submit();
}


function updateHiddenInputs(form, name, values) {

    var existingInputs = form.querySelectorAll(`input[name="${name}"]`);
    existingInputs.forEach(input => input.remove());

 
    values.forEach(value => {
        var input = document.createElement('input');
        input.type = 'hidden';
        input.name = name;
        input.value = value;
        form.appendChild(input);
    });
}
function showMoreTags() {
    var hiddenTags = document.querySelectorAll('.form-select .hidden');
    hiddenTags.forEach(function (tag) {
        tag.classList.remove('hidden');
    });
    document.querySelector('.show-more').style.display = 'none';
}