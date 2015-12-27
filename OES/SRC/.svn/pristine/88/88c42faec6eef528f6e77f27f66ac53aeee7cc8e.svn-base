class Alerter
 @DEBUG_=true
 @key_value: (key ,value) ->
     if @DEBUG_ then alert "#{key}:#{value}"
  @info: (msg="") ->
     if @DEBUG_ then alert msg
 @list:()->
     if @DEBUG_ then   alert s for s in arguments
 @checkFn:(fn)->
      if @DEBUG_ then  fn.apply(this, arguments)
 @turnOn: ->
    @DEBUG_=true
 @turnOff: ->
    @DEBUG_=false
  @isOn: ->
    @DEBUG_
