var Alerter;
Alerter = (function () {
    function Alerter() { }
    Alerter.DEBUG_ = true;
    Alerter.key_value = function (key, value) {
        if (this.DEBUG_) {
            return alert("" + key + ":" + value);
        }
    };
    Alerter.info = function (msg) {
        if (msg == null) {
            msg = "";
        }
        if (this.DEBUG_) {
            return alert(msg);
        }
    };
    Alerter.list = function () {
        var s, _i, _len, _results;
        if (this.DEBUG_) {
            _results = [];
            for (_i = 0, _len = arguments.length; _i < _len; _i++) {
                s = arguments[_i];
                _results.push(alert(s));
            }
            return _results;
        }
    };
    Alerter.checkFn = function (fn) {
        if (this.DEBUG_) {
            return fn.apply(this, arguments);
        }
    };
    Alerter.turnOn = function () {
        return this.DEBUG_ = true;
    };
    Alerter.turnOff = function () {
        return this.DEBUG_ = false;
    };
    Alerter.isOn = function () {
        return this.DEBUG_;
    };
    return Alerter;
})();