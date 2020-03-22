require 'ruby2d'
require_relative 'vector3'
set width: 1200
set height: 800
set background: 'white'


class AnyShape
  def initialize(x = nil, y = nil)
    @x = x || rand(Window.width)
    @y = y || rand(Window.height)
    @x_velocity = (1..15).to_a.sample
    @y_velocity = (1..15).to_a.sample
    @color = Color.new('random')
  end

  def direction
    # Vector3.new(Window.mouse_x, Window.mouse_y).direction_to(Vector3.new(@x, @y))
    Vector3.new(@x, @y).direction_to(Vector3.new(Window.mouse_x, Window.mouse_y)) * ((distance_between < (300)) ? Vector3.new(-1,-1,-1) : Vector3.new(1,1,1)) 
  end

  def distance_between
    Vector3.distance_between(Vector3.new(Window.mouse_x, Window.mouse_y), Vector3.new(@x, @y))
  end

  def move
    # @x = ((@x + @x_velocity) % Window.width)
    # @y = ((@y + @y_velocity) % Window.width)
    # @x += (@x_velocity)
    # @y += (@y_velocity)
    
    @x =  (@x + (direction.x * @x_velocity)) % Window.width
    @y =  (@y + (direction.y * @y_velocity)) % Window.height
    
  end
  def draw
    shape.new(x: @x, y: @y, size: 20, color: @color)
  end
  def change_x_direction
    @x_velocity = - @x_velocity
  end
  def change_y_direction
    @y_velocity = - @y_velocity
  end
  def change_direction
    @x_velocity = - @x_velocity
    @y_velocity = - @y_velocity
  end
end

class SquareShape < AnyShape
  def shape; Square end;
end
class CircleShape < AnyShape
  def shape; Circle end;
end

@boxes = Array.new(40) { [SquareShape].sample.new }

update do
  clear
  @boxes.each do |box|
    Square.new(x: Window.mouse_x, y: Window.mouse_y, size: 10, color: 'red')
    box.move
    box.draw
  end
  
end
show