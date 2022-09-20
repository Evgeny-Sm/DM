
export function initializeFileDropZone(dropZoneElement, inputFileContainer) {
    const inputFile = inputFileContainer.querySelector("input");

    function onDragHover(e) {
        e.preventDefault();
        dropZoneElement.classList.add("hover");
    }

    function onDragLeave(e) {
        e.preventDefault();
        dropZoneElement.classList.remove("hover");
    }

    function onDrop(e) {
        e.preventDefault();
        dropZoneElement.classList.remove("hover");

        inputFile.files = e.dataTransfer.files;
        const event = new Event('change', { bubbles: true });
        inputFile.dispatchEvent(event);
    }

    function onPaste(e) {
        inputFile.files = e.clipboardData.files;
        const event = new Event('change', { bubbles: true });
        inputFile.dispatchEvent(event);
    }

    dropZoneElement.addEventListener("dragenter", onDragHover);
    dropZoneElement.addEventListener("dragover", onDragHover);
    dropZoneElement.addEventListener("dragleave", onDragLeave);
    dropZoneElement.addEventListener("drop", onDrop);
    dropZoneElement.addEventListener('paste', onPaste);

    return {
        dispose: () => {
            dropZoneElement.addEventListener('dragenter', onDragHover);
            dropZoneElement.addEventListener('dragover', onDragHover);
            dropZoneElement.addEventListener('dragleave', onDragLeave);
            dropZoneElement.addEventListener("drop", onDrop);
            dropZoneElement.addEventListener('paste', onPaste);
        }

    }

}