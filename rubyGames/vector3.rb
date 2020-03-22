class Vector3
  attr_reader :x, :y, :z
  def initialize(x = 0, y = 0, z = 0)
    @x = x
    @y = y
    @z = z
  end

  def self.forward
    Vector3.new(0, 0, 1)
  end

  def self.up
    Vector3.new(0, 1, 0)
  end

  def self.right
    Vector3.new(1, 0, 0)
  end

  def self.distance_between(vectorA, vectorB)
    Math.sqrt((vectorB.x - vectorA.x) ** 2 + (vectorB.y - vectorA.y) ** 2 + (vectorB.z - vectorA.z) ** 2)
  end

  def self.direction(vectorA, vectorB)
    normalized = distance_between(vectorA, vectorB)
    direction_vector = Vector3.new((vectorB.x - vectorA.x)/normalized,  (vectorB.y - vectorA.y)/normalized, (vectorB.z - vectorA.z)/normalized)
    direction_vector
  end

  def inspect
    "{#{x}, #{y}, #{z}}"
  end

  def vector?
    true
  end

  def *(value)
    if(value.respond_to?(:vector?))
      vector_dot_product(value)
    elsif(value.respond_to?(:*))
      scalar_vector_dot_product(value)
    else
      raise "Unexpected Type #{value.class} Expected a Vector3 or Integer or Float"
    end
  end

  def magnitude
    Math.sqrt(@x ** 2 + @y ** 2 + @z ** 2)
  end

  def direction
    Vector3.direction(Vector3.new(0, 0, 0), self)
  end

  def direction_to(vector)
    Vector3.direction(self, vector)
  end

  private

  def scalar_vector_dot_product(value)
    @x * value + @y * value + @z * value
  end

  def vector_dot_product(vector)
    Vector3.new(@x * vector.x, @y * vector.y, @z * vector.z)
  end
end
# x = Vector3.new(3, 4, 0)
#Vector3.direction(Vector3.new(3, 4, 1), Vector3.new(0, 0, 0))