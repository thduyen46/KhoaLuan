(function ($) {
	"use strict";
	LoadEditor();
})(jQuery);

function LoadEditor() {
	var editorElements = document.querySelectorAll('.editor');
	console.log('RUNN');
	editorElements.forEach(function (editorElement) {
		if (!editorElement.getAttribute('data-ckeditor-initialized')) {
			CKEDITOR.replace(editorElement, {
				height: 500,
				filebrowserUploadMethod: "form",
				filebrowserUploadUrl: window.location.origin + '/Image/UploadImage',
				filebrowserBrowseUrl: window.location.origin + '/Image/UploadExplorer'
			});
			editorElement.setAttribute('data-ckeditor-initialized', 'true');
		}
	});
}

