class Interop {
    public hello() {
        return alert('Hello');
    }
    public showPrompt(message: string): string {
        return prompt(message, 'Type anything here');
    }
}

function Load() {
    window['interop'] = new Interop();
}
Load();