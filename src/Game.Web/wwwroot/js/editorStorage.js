window.editorStorage = {
  copyText: async function (text) {
    if (!navigator.clipboard) {
      return false;
    }

    await navigator.clipboard.writeText(text);
    return true;
  }
};
