
    $(document).ready(function () {
        $('#confirm_delete').on('show.bs.modal', function (event) {
            var button = $(event.relatedTarget); // Button that triggered the modal
            var postId = button.data('post-id'); // Extract post_id from data-post-id attribute
            var modal = $(this);

            // Set the href attribute of the delete button in the modal to include the postId
            modal.find('#deleteButton').attr('href', '/Post/Delete/' + postId);
        });
    });