(print "Hello, World")

(print (+ 1 (- 5 3)))

(defun factorial (n)
	(if (zerop n) 1
		(* n (factorial (- n 1)))))

(print (factorial 5))

(defun fibonacci (n)
	(if (> n 1)
		(+ (fibonacci (- n 1))
			(fibonacci (- n 2)))
		n))

(print (fibonacci 5))
