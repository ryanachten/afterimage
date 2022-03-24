var Interop = /** @class */ (function () {
    function Interop() {
    }
    Interop.prototype.hello = function () {
        return alert('Hello');
    };
    Interop.prototype.showPrompt = function (message) {
        return prompt(message, 'Type anything here');
    };
    return Interop;
}());
function Load() {
    console.log("loading javascript main");
    window['interop'] = new Interop();
}
Load();
//# sourceMappingURL=main.js.map