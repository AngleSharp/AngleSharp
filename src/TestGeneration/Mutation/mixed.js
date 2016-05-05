suite('JsMutationObserver mixed types', function() {

  test('attr and characterData', function() {
    var div = document.createElement('div');
    var text = div.appendChild(document.createTextNode('text'));
    var observer = new JsMutationObserver(function() {});
    observer.observe(div, {
      attributes: true,
      characterData: true,
      subtree: true
    });
    div.setAttribute('a', 'A');
    div.firstChild.data = 'changed';

    var records = observer.takeRecords();
    assert.strictEqual(records.length, 2);

    expectRecord(records[0], {
      type: 'attributes',
      target: div,
      attributeName: 'a',
      attributeNamespace: null
    });
    expectRecord(records[1], {
      type: 'characterData',
      target: div.firstChild
    });
  });

});