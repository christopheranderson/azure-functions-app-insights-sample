var items = [];

var get = function(cb) {
    setTimeout(() => {
        cb(items);
    }, 1000)
}

var add = function(item, cb) {
    setTimeout(() => {
        items.push(item);
        cb();
    }, 5000) 
}

module.exports = {
    get: get,
    add: add
}