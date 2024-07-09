document.addEventListener("DOMContentLoaded", function () {
    function setupAddRemoveButtons(addButtonId, removeButtonId, containerId) {
        var addButton = document.getElementById(addButtonId);
        var removeButton = document.getElementById(removeButtonId);
        var container = document.getElementById(containerId);

        addButton.addEventListener("click", function () {
            var newInput = container.querySelector("input").cloneNode(true);
            newInput.value = "";
            container.appendChild(newInput);
        });

        removeButton.addEventListener("click", function () {
            var inputs = container.querySelectorAll("input");
            if (inputs.length > 1) {
                container.removeChild(inputs[inputs.length - 1]);
            }
        });
    }

    setupAddRemoveButtons("addPlatformButton", "removePlatformButton", "platformsContainer");
    setupAddRemoveButtons("addTagButton", "removeTagButton", "tagsContainer");
    setupAddRemoveButtons("addGenreButton", "removeGenreButton", "genresContainer");
    setupAddRemoveButtons("addScreenshotButton", "removeScreenshotButton", "screenshotContainer");
    
});

function showMoreTags() {
    var hiddenTags = document.querySelectorAll('.form-select .hidden');
    hiddenTags.forEach(function (tag) {
        tag.classList.remove('hidden');
    });
    document.querySelector('.show-more').style.display = 'none';
}