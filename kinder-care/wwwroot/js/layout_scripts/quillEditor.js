// ========================== Quill Editor Initialization Script ===========================
function editorFunction(editorId) {
    const quill = new Quill(editorId, {
        theme: 'snow'
    });
}

editorFunction('#editor');
editorFunction('#editorTwo');
