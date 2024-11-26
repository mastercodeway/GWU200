document.querySelector(".delete-password").addEventListener("click", function () {
    let showDeleteModal = document.getElementById("modal-delete");
    showDeleteModal.style.display = "block";

});

document.querySelector(".change-password").addEventListener("click", function () {
    let showCreatePswModal = document.getElementById("modal-change-password");
    showCreatePswModal.style.display = "block";
});
document.getElementById("cancel-delete").addEventListener("click", function () {
    let showDeleteModal = document.getElementById("modal-delete");
    showDeleteModal.style.display = "none";
});
document.getElementById("cancel-change-password").addEventListener("click", function () {
    let showCreatePswModal = document.getElementById("modal-change-password");
    showCreatePswModal.style.display = "none";
}); 
